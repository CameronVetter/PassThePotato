namespace PassThePotato.MessageContracts
{
    public interface IStats
    {
        int CurrentUsersUpdates { get; set; }
        int MostSimulatneousUsers { get; set; }
        int Errors { get; set; }
        int PassesOfThePotato { get; set; }
        int CurrentUsersRequest { get; set; }
        int WhoCanIPassToRequest { get; set; }
        int WhoHasPotatoRequest { get; set; }
        int WhoYouCanPassToUpdates { get; set; }
        int WhoHasPotatoUpdates { get; set; }
    }
}
