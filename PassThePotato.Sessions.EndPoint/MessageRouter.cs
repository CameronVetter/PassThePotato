using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using PassThePotato.MessageContracts;

namespace PassThePotato.Sessions.EndPoint
{
    public class MessageRouter
    {
        private static MessageRouter _instance;

        public static MessageRouter GetRouter => _instance ?? (_instance = new MessageRouter());

        private readonly IHubContext _hub;
        private readonly Queue<object> _messagesToRoute;

        private MessageRouter()
        {
            _hub = GlobalHost.ConnectionManager.GetHubContext<ResponseHub>();
            _messagesToRoute = new  Queue<object>();
        }

        public void RouteMessage(object message)
        {
            lock (_messagesToRoute)
            {
                _messagesToRoute.Enqueue(message);
            }

            SendMessages();
        }

        private void SendMessages()
        {
            lock (_messagesToRoute)
            {
                    while (_messagesToRoute.Count > 0)
                    {
                        var message = _messagesToRoute.Dequeue();
                        if (message != null)
                        {
                            SendMessageToClient(message);
                        }
                    }
            }
        }

        private void SendMessageToClient(object message)
        {
            
            if (message.GetType().Name == nameof(IWhoHasThePotato))
            {
                var msg = message as IWhoHasThePotato;
                if (msg != null)
                {
                    _hub.Clients.All.whoHasPotato(msg.PotatoHolderId, msg.PotatoHolderName);
                }
            }
            else
            if (message.GetType().Name == nameof(IWhoCanYouPassTo))
            {
                var msg = message as IWhoCanYouPassTo;
                if (msg != null)
                {
                    _hub.Clients.All.whoCanYouPassTo(msg.RequestorId, msg.LeftId, msg.LeftName, msg.RightId, msg.RightName);
                }
            }
            else
            if (message.GetType().Name == nameof(IError))
            {
                var msg = message as IError;
                if (msg != null)
                {
                    _hub.Clients.Client(msg.ConnectionId).Send("Server", msg.ErrorMessage);
                }
            }
            else
            {
                throw new Exception($"Unknown Message Type can't be routed {message.GetType().Name}");
            }
        }

    }
}