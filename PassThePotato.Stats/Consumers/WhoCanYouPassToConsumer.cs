using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Consumers
{
    public class WhoCanYouPassToConsumer : IConsumer<IWhoCanYouPassTo>
    {
        public async Task Consume(ConsumeContext<IWhoCanYouPassTo> context)
        {
            Stats.WhoYouCanPassToUpdates++;
            await Stats.SendStats();
        }
    }
}