using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Sessions.EndPoint.Consumers
{
    public class ErrorConsumer : IConsumer<IError>
    {
        public async Task Consume(ConsumeContext<IError> context)
        {
            MessageRouter.GetRouter.RouteMessage(context.Message);
            await Task.FromResult(0);
        }
    }
}