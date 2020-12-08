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
            var scanDocumentType = formatType switch
            {
                IDocument.FormatType.TXT => "Text",
                IDocument.FormatType.PDF => "PDF",
                _ => "Image"
            };

            string name = $"{scanDocumentType}Scan{ScanCounter + 1}.{formatType.ToString().ToLower()}";

            if (formatType == IDocument.FormatType.PDF)
                document = new PDFDocument(name);
            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument(name);
            else
                document = new TextDocument(name);

            switch (state)
            {
                case IDevice.State.@on:
                    ScanCounter++;
                    Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
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