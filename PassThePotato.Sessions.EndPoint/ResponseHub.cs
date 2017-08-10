using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using PassThePotato.MessageContracts;
using PassThePotato.Sessions.EndPoint.Messages;

namespace PassThePotato.Sessions.EndPoint
{

    // TODO https://www.asp.net/signalr/overview/guide-to-the-api/hubs-api-guide-server#stronglytypedhubs

    public class ResponseHub : Hub
    {
        #region Lifecycle

        public override async Task OnConnected()
        {
            HandleConnection();

            await base.OnConnected();
            Send("Server", $"{HubSessionManager.GetManager.GetSession(Context.ConnectionId).Name} connected");
            HubSessionManager.GetManager.PublishUsersUpdate();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var name = HubSessionManager.GetManager.GetSession(Context.ConnectionId).Name;
            if (stopCalled)
            {
                HubSessionManager.GetManager.DeleteSession(Context.ConnectionId);
            }
            else
            {
                HubSessionManager.GetManager.GetSession(Context.ConnectionId).Connected = false;
            }

            await base.OnDisconnected(stopCalled);
            Send("Server", $"{name} disconnected");
            HubSessionManager.GetManager.PublishUsersUpdate();

        }

        public override async Task OnReconnected()
        {
            HandleConnection();

            await base.OnReconnected();
            Send("Server", $"{HubSessionManager.GetManager.GetSession(Context.ConnectionId).Name} reconnected");
            HubSessionManager.GetManager.PublishUsersUpdate();
        }

        private void HandleConnection()
        {

            if (HubSessionManager.GetManager.Exists(Context.ConnectionId))
            {
                HubSessionManager.GetManager.GetSession(Context.ConnectionId).Connected = true;
            }
            else
            {
                HubSessionManager.GetManager.CreateSession(GetNameFromQueryString(), Context.ConnectionId);
            }
        }


        #endregion


        #region Client Side

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public void WhoCanIPassTo()
        {
            ThreadPool.QueueUserWorkItem(stateInfo =>
            {
                ServiceBus.Bus.Publish((IRequesstWhoCanIPassTo)new RequestWhoCanIPassTo(Context.ConnectionId));
            });
        }

        public void PassThePotato(string passerId, string receiverId)
        {
            ThreadPool.QueueUserWorkItem(stateInfo =>
            {
                ServiceBus.Bus.Publish((IPassPotato)new PassPotato(passerId, receiverId));
            });
        }


        #endregion

            private string GetNameFromQueryString()
        {
            var id = Context.QueryString["name"];

            if (id == null || id == "undefined")
            {
                return null;
            }

            return id;
        }



    }
}