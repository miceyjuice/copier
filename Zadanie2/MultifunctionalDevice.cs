using Zadanie1;
using System;

namespace Zadanie2
{
    public class MultifunctionalDevice : Copier, IFax
    {
        private int _sendCounter = 0;
        private int _receiveCounter = 0;
        private int _counter = 0;
        private string _faxNumber;


        public int SendCounter
        {
            get => _sendCounter;
            set => _sendCounter = value;
        }

        public int ReceiveCounter
        {
            get => _receiveCounter;
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

        public MultifunctionalDevice(string faxNumber)
        {
            FaxNumber = faxNumber;
        }
        
        public void PowerOn()
        {
            _counter++;
            base.PowerOn();
        }

        public void Send(out IDocument doc, string faxNumber)
        {
            Scan(out doc);
            switch (state)
            {
                case IDevice.State.@on:
                    _sendCounter++;
                    Console.WriteLine($"{ DateTime.Now } Sent: { doc.GetFileName() } from: { this._faxNumber } to: { faxNumber }");
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Receive(in IDocument doc, string faxNumber)
        {
            Print(in doc);
            
            switch (state)
            {
                case IDevice.State.@on:
                    _receiveCounter++;
                    Console.WriteLine($"{ DateTime.Now } Received: { doc.GetFileName() } from: { faxNumber }");
                    break;
                case IDevice.State.off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}