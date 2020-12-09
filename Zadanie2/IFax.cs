using Zadanie1;

namespace Zadanie2
{
    public interface IFax : IDevice
    { 
        /// <summary>
        /// Dokument jest wysyłany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        /// <param name="faxNumber">obiekt typu String, różny od `null`</param>
        void Send(out IDocument document, string faxNumber);

        /// <summary>
        /// Dokument jest przyjmowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        /// <param name="faxNumber">obiekt typu String, różny od `null`</param>
        void Receive(in IDocument document, string faxNumber);
    }
}