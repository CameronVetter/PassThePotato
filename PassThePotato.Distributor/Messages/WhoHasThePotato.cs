using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor.Messages
{
    public class WhoHasThePotato : IWhoHasThePotato
    {
        public string PotatoHolderId { get; }
        public string PotatoHolderName { get; }

        public WhoHasThePotato(string id, string name)
        {
            PotatoHolderId = id;
            PotatoHolderName = name;
        }
    }
}