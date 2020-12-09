using System;

namespace Zadanie3
{
    public class MultidimensionalDevice : BaseDevice
    {
        private int _sendCounter = 0;
        private int _receiveCounter = 0;
        private int _counter = 0;
        private string _faxNumber;
        
        private IFax Fax { get; set; }
        private IPrinter Printer { get; set; }
        private IScanner Scanner { get; set; }
        
        public int SendCounter
        {
            get => Fax.SendCounter;
            set => _sendCounter = value;
        }

        public int ReceiveCounter
        {
            get => Fax.ReceiveCounter;
            set => _receiveCounter = value;
        }

        public int Counter
        {
            get => _counter;
            private set => _counter = value;
        }

        public string FaxNumber
        {
            get => _faxNumber;
            set => _faxNumber = value;
        }

        public MultidimensionalDevice()
        {
            Printer = new Printer();
            Scanner = new Scanner();
            Fax = new Fax();
        }
        
        public void PowerOn()
        {
            _counter++;
            base.PowerOn();
        }

        public void PrinterOn()
        {
            switch (state)
            {
                case IDevice.State.@on:
                    Printer.PowerOn();
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        } 
        public void PrinterOff() => Printer.PowerOff();

        public void ScannerOn()
        {
            switch (state)
            {
                case IDevice.State.@on:
                    Scanner.PowerOn();
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ScannerOff() => Scanner.PowerOff();
        
        public void FaxOn()
        {
            switch (state)
            {
                case IDevice.State.@on:
                    Fax.PowerOn();
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void FaxOff() => Fax.PowerOff();
        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG) => Scanner.Scan(out document, formatType);
        public void Print(in IDocument document) => Printer.Print(in document);

        public void Send(out IDocument doc, string faxNumber)
        {
            Scan(out doc);
            
            switch (state)
            {
                case IDevice.State.@on:
                    Fax.Send(doc, faxNumber);
                    break;
                case IDevice.State.off:
                    Console.WriteLine("Can't send a document when Scanner is turned off!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Receive(in IDocument doc, string faxNumber)
        {
            switch (state)
            {
                case IDevice.State.@on:
                    Fax.Receive(doc, faxNumber);
                    Print(doc);
                    break;
                case IDevice.State.off:
                    Console.WriteLine("Can't receive a document when Printer is turned off!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}