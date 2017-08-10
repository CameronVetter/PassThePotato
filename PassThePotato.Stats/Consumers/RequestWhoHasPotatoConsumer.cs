using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Consumers
{
    public class RequestWhoHasPotatoConsumer : IConsumer<IRequestWhoHasPotato>
    {
        public async Task Consume(ConsumeContext<IRequestWhoHasPotato> context)
        {
            Stats.WhoHasPotatoRequest++;
            await Stats.SendStats();
        }
    }
}