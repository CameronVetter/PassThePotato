using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Sessions.EndPoint.Consumers
{
    public class WhoHasThePotatoConsumer : IConsumer<IWhoHasThePotato>
    {
        public async Task Consume(ConsumeContext<IWhoHasThePotato> context)
        {
            MessageRouter.GetRouter.RouteMessage(context.Message);
            await Task.FromResult(0);
        }
    }
}