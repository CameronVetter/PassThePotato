namespace PassThePotato.MessageContracts
{
    public interface IWhoCanYouPassTo
    {
        string RequestorId { get; }
        string LeftId { get; }
        string LeftName { get; }
        string RightId { get; }
        string RightName { get; }
    }
}
