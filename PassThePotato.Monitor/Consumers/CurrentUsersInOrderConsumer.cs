using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Monitor.Consumers
{
    public class CurrentUsersInOrderConsumer : IConsumer<ICurrentUsersInOrder>
    {
        public async Task Consume(ConsumeContext<ICurrentUsersInOrder> context)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {

                ((MainWindow) Application.Current.MainWindow).UpdatePlayers(context.Message.Users.ToList());
            });
            await Task.FromResult(0);
        }
    }
}
