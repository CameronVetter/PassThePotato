using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor.Consumers 
{
    public class RequestWhoCanIPassToConsumer : IConsumer<IRequesstWhoCanIPassTo>
    {
        public async Task Consume(ConsumeContext<IRequesstWhoCanIPassTo> context)
        {
            await PotatoManager.WhoCanIPassTo(context.Message.RequestorId);
        }
    }
}
