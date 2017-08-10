using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Consumers
{
    public class PassPotatoConsumer : IConsumer<IPassPotato>
    {
        public async Task Consume(ConsumeContext<IPassPotato> context)
        {
            Stats.PassesOfThePotato++;
            await Stats.SendStats();
        }
    }
}