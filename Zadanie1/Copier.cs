using System;
using Zadanie1;

namespace Zadanie1
{
    public class Copier : BaseDevice, IScanner, IPrinter
    {
        private int _printCounter = 0;
        private int _scanCounter = 0;
        private int _counter = 0;

        public int PrintCounter
        {
            get => _printCounter;
            private set => _printCounter = value;
        }

        public int ScanCounter
        {
            get => _scanCounter;
            private set => _scanCounter = value;
        }

        public new int Counter
        {
            get => _counter;
            private set => _counter = value;
        }

        public void PowerOn()
        {
            _counter++;
            base.PowerOn();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = formatType switch
            {
                IDocument.FormatType.PDF => new PDFDocument($"PDFScan{ _scanCounter }.pdf"),
                IDocument.FormatType.TXT => new TextDocument($"TextScan{ _scanCounter }.txt"),
                IDocument.FormatType.JPG => new ImageDocument($"ImageScan{ _scanCounter }.jpg"),
                _ => throw new FormatException()
            };

            switch (state)
            {
                case IDevice.State.@on:
                    _scanCounter++;
                    Console.WriteLine($"{ DateTime.Now } Scan: { document.GetFileName() }");
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Print(in IDocument document)
        {
            switch (state)
            {
                case IDevice.State.@on:
                    PrintCounter++;
                    Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ScanAndPrint()
        {
            Scan(out IDocument doc);
            Print(doc);
        }
    }
}