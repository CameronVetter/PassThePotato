using System.Threading.Tasks;
using System.Windows;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Monitor.Consumers
{
    public class StatsConsumer : IConsumer<IStats>
    {
        public async Task Consume(ConsumeContext<IStats> context)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                ((MainWindow)Application.Current.MainWindow).UpdateStats(context.Message);
            });
            await Task.FromResult(0);
        }
    }
}
