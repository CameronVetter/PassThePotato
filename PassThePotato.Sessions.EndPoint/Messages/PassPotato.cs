using PassThePotato.MessageContracts;

namespace PassThePotato.Sessions.EndPoint.Messages
{
    public class PassPotato : IPassPotato
    {
        public string PasserId { get; }

        public string ReceieverId { get; }

        public PassPotato(string passerId, string receiverId)
        {
            PasserId = passerId;
            ReceieverId = receiverId;
        }
    }
}