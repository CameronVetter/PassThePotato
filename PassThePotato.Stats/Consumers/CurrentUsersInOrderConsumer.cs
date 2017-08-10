using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Stats.Consumers
{
    public class CurrentUsersInOrderConsumer : IConsumer<ICurrentUsersInOrder>
    {
        public async Task Consume(ConsumeContext<ICurrentUsersInOrder> context)
        {
            Stats.CurrentUsersUpdates++;
            if (context.Message.Users.Count > Stats.MostSimulatneousUsers)
                Stats.MostSimulatneousUsers = context.Message.Users.Count;
            await Stats.SendStats();
        }
    }
}