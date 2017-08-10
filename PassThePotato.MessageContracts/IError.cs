namespace PassThePotato.MessageContracts
{
    public interface IError
    {
        string ConnectionId { get; }
        string ErrorMessage { get; }
    }
}
