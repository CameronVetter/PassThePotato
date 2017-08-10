using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor.Consumers
{
    public class PassPotatoConsumer : IConsumer<IPassPotato>
    {
        public async Task Consume(ConsumeContext<IPassPotato> context)
        {
            await PotatoManager.PassPotato(context.Message.PasserId, context.Message.ReceieverId);
        }
    }
}