using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor.Consumers
{
    public class CurrentUsersInOrderConsumer : IConsumer<ICurrentUsersInOrder>
    {
        public async Task Consume(ConsumeContext<ICurrentUsersInOrder> context)
        {
            PotatoManager.UpdateUsers(context.Message.Users.ToList());
            await PotatoManager.CheckPotatoState();
        }
    }
}