using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Messages
{
    public class Stats : IStats
    {
        public int CurrentUsersUpdates { get; set; }
        public int MostSimulatneousUsers { get; set; }
        public int Errors { get; set; }
        public int PassesOfThePotato { get; set; }
        public int CurrentUsersRequest { get; set; }
        public int WhoCanIPassToRequest { get; set; }
        public int WhoHasPotatoRequest { get; set; }
        public int WhoYouCanPassToUpdates { get; set; }
        public int WhoHasPotatoUpdates { get; set; }
    }
}