using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Consumers
{
    public class WhoHasThePotatoConsumer : IConsumer<IWhoHasThePotato>
    {
        public async Task Consume(ConsumeContext<IWhoHasThePotato> context)
        {
            Stats.WhoHasPotatoUpdates++;
            await Stats.SendStats();
        }
    }
}