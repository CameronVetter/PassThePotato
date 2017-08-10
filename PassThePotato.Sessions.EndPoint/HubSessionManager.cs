using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PassThePotato.MessageContracts;
using PassThePotato.Sessions.EndPoint.Messages;

namespace PassThePotato.Sessions.EndPoint
{
    public class HubSessionManager
    {
        private static HubSessionManager _instance;

        public static HubSessionManager GetManager => _instance ?? (_instance = new HubSessionManager());

        private HubSessionManager()
        { }

        private readonly Dictionary<string, SessionClient> _sessions = new Dictionary<string, SessionClient>();

        public int Count()
        {
            return _sessions.Count;
        }

        public bool Exists(string connectionId)
        {
            return _sessions.ContainsKey(connectionId);
        }

        public SessionClient GetSession(string connectionId)
        {
            return Exists(connectionId) ? _sessions[connectionId] : null;
        }

        public SessionClient CreateSession(string name, string connectionId)
        {
            if (Exists(connectionId))
            {
                return null;
            }

            var newSession = new SessionClient(connectionId, name)
            {
                Connected = true
            };
            _sessions.Add(connectionId, newSession);
            return newSession;
        }

        public void DeleteSession(string connectionId)
        {
            if (Exists(connectionId))
            {
                _sessions.Remove(connectionId);
            }
        }

        public IList<Tuple<string, string>> GetUsersAsTuple()
        {
            return _sessions
                .Where(session => session.Value.Connected)
                .Select(session => new Tuple<string, string>(session.Value.ConnectionId, session.Value.Name)) 
                .ToList();
        }

        public void PublishUsersUpdate()
        {
            ThreadPool.QueueUserWorkItem(async stateInfo =>
            {
                await ServiceBus.Bus.Publish((ICurrentUsersInOrder)new CurrentUsersInOrder());
            });
        }
    }
}