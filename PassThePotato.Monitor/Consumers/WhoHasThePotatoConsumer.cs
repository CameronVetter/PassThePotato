using System.Threading.Tasks;
using System.Windows;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Monitor.Consumers
{
    public class WhoHasThePotatoConsumer : IConsumer<IWhoHasThePotato>
    {
        public async Task Consume(ConsumeContext<IWhoHasThePotato> context)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                ((MainWindow)Application.Current.MainWindow).UpdatePotatoHolder(context.Message.PotatoHolderId, context.Message.PotatoHolderName);
            });
            await Task.FromResult(0);
        }
    }
}
