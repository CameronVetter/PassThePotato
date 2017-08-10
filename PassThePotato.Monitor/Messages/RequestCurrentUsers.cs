using PassThePotato.MessageContracts;

namespace PassThePotato.Monitor.Messages
{
    public class RequestCurrentUsers : IRequestCurrentUsers
    {
        public string RequestorId { get; }

        public RequestCurrentUsers(string id)
        {
            RequestorId = id;
        }
    }
}
