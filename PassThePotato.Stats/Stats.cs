using System.Threading.Tasks;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats
{
    public static class Stats
    {
        public static int CurrentUsersUpdates { get; set; }
        public static int MostSimulatneousUsers { get; set; }
        public static int Errors { get; set; }
        public static int PassesOfThePotato { get; set; }
        public static int CurrentUsersRequest { get; set; }
        public static int WhoCanIPassToRequest { get; set; }
        public static int WhoHasPotatoRequest { get; set; }
        public static int WhoYouCanPassToUpdates { get; set; }
        public static int WhoHasPotatoUpdates { get; set; }

        public static async Task SendStats()
        {
            await ServiceBus.Bus.Publish((IStats) new Messages.Stats
            {
                PassesOfThePotato = PassesOfThePotato,
                WhoHasPotatoUpdates = WhoHasPotatoUpdates,
                CurrentUsersRequest = CurrentUsersRequest,
                Errors = Errors,
                WhoCanIPassToRequest = WhoCanIPassToRequest,
                WhoHasPotatoRequest = WhoHasPotatoRequest,
                WhoYouCanPassToUpdates = WhoYouCanPassToUpdates,
                MostSimulatneousUsers = MostSimulatneousUsers,
                CurrentUsersUpdates = CurrentUsersUpdates
            });
        }
    }
}