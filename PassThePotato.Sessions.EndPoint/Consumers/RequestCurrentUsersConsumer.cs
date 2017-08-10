using System.Threading.Tasks;
using MassTransit;
using PassThePotato.MessageContracts;

namespace PassThePotato.Sessions.EndPoint.Consumers
{
    public class RequestCurrentUsersConsumer : IConsumer<IRequestCurrentUsers>
    {
        public async Task Consume(ConsumeContext<IRequestCurrentUsers> context)
        {
            HubSessionManager.GetManager.PublishUsersUpdate();
            await Task.FromResult(0);
        }
    }
}
