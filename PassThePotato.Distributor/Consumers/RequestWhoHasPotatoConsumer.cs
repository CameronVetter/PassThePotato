using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor.Consumers
{
    public class RequestWhoHasPotatoConsumer : IConsumer<IRequestWhoHasPotato>
    {
        public async Task Consume(ConsumeContext<IRequestWhoHasPotato> context)
        {
            await PotatoManager.CheckPotatoState();
        }
    }
}