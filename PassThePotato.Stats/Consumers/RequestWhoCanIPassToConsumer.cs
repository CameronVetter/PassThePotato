using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Consumers 
{
    public class RequestWhoCanIPassToConsumer : IConsumer<IRequesstWhoCanIPassTo>
    {
        public async Task Consume(ConsumeContext<IRequesstWhoCanIPassTo> context)
        {
            Stats.WhoCanIPassToRequest++;
            await Stats.SendStats();
        }
    }
}
