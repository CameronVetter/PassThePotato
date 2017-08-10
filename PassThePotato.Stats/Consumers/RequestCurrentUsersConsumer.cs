using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Consumers
{
    public class RequestCurrentUsersConsumer : IConsumer<IRequestCurrentUsers>
    {
        public async Task Consume(ConsumeContext<IRequestCurrentUsers> context)
        {
            Stats.CurrentUsersRequest++;
            await Stats.SendStats();
        }
    }
}
