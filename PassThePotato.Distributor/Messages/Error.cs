using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor.Messages
{
    public class Error : IError
    {
        public string ConnectionId { get; }

        public string ErrorMessage { get; }

        public Error(string connectionId, string errorMessage)
        {
            ConnectionId = connectionId;
            ErrorMessage = errorMessage;
        }
    }
}