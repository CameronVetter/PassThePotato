using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Sessions.EndPoint.Consumers
{
    public class WhoCanYouPassToConsumer : IConsumer<IWhoCanYouPassTo>
    {
        public async Task Consume(ConsumeContext<IWhoCanYouPassTo> context)
        {
            MessageRouter.GetRouter.RouteMessage(context.Message);
            await Task.FromResult(0);
        }
    }
}