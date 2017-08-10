using PassThePotato.MessageContracts;

namespace PassThePotato.Monitor.Messages
{
    public class RequestWhoHasPotato : IRequestWhoHasPotato
    {
        public string RequestorId { get; }

        public RequestWhoHasPotato(string id)
        {
            RequestorId = id;
        }
    }
}
