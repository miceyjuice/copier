using System;
using Zadanie1;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var device = new MultifunctionalDevice("12 231 22 00");
            device.PowerOn();

            IDocument document1;
            IDocument document2 = new ImageDocument("okay.jpg");
            
            device.Send(out document1, "12 232 21 01");
            device.Send(out document1, "12 232 21 02");
            device.Send(out document1, "12 232 21 03");
            device.Receive(in document2, "12 240 11 10");
            
            Console.WriteLine("How many times device was turned on: {0}, document was sent: {1}, document was received: {2}", device.Counter, device.SendCounter, device.ReceiveCounter);
        }
    }
}