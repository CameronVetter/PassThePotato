using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Consumers
{
    public class ErrorConsumer : IConsumer<IError>
    {
        public async Task Consume(ConsumeContext<IError> context)
        {
            Stats.Errors++;
            await Stats.SendStats();
        }
    }
}