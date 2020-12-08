using Zadanie1;
using System;

namespace Zadanie2
{
    public class MultifunctionalDevice : BaseDevice, IPrinter, IScanner, IFax
    {
        private int _printCounter = 0;
        private int _scanCounter = 0;
        private int _sendCounter = 0;
        private int _counter = 0;
        private string _faxNumber;

        public int PrintCounter
        {
            get => _printCounter;
            set => _printCounter = value;
        }

        public int ScanCounter
        {
            get => _scanCounter;
            set => _scanCounter = value;
        }

        public int SendCounter
        {
            get => _sendCounter;
            set => _sendCounter = value;
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

        public MultifunctionalDevice(string faxNumber)
        {
            FaxNumber = faxNumber;
        }

        public void PowerOn()
        {
            _counter++;
            base.PowerOn();
        }

        public void Print(in IDocument document)
        {
            switch (state)
            {
                case IDevice.State.@on:
                    _printCounter++;
                    Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            var scanDocumentType = formatType switch
            {
                IDocument.FormatType.TXT => "Text",
                IDocument.FormatType.PDF => "PDF",
                _ => "Image"
            };

            var name = $"{scanDocumentType}Scan{ScanCounter + 1}.{formatType.ToString().ToLower()}";

            if (formatType == IDocument.FormatType.PDF)
                document = new PDFDocument(name);
            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument(name);
            else
                document = new TextDocument(name);

            switch (state)
            {
                case IDevice.State.@on:
                    _scanCounter++;
                    Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ScanAndPrint()
        {
            Scan(out IDocument document);
            Print(document);
        }

        public void Send(IDocument doc, string faxNumber)
        {
            switch (state)
            {
                case IDevice.State.@on:
                    _sendCounter++;
                    Console.WriteLine($"{DateTime.Now} Sent: {doc.GetFileName()} from: {this._faxNumber} to: {faxNumber}");
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}