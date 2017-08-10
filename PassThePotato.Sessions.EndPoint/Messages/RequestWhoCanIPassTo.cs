using PassThePotato.MessageContracts;

namespace PassThePotato.Sessions.EndPoint.Messages
{
    public class RequestWhoCanIPassTo : IRequesstWhoCanIPassTo
    {
        public string RequestorId { get; }

        public RequestWhoCanIPassTo(string id)
        {
            RequestorId = id;
        }
    }
}