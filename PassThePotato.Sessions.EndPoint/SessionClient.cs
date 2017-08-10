using System;

namespace PassThePotato.Sessions.EndPoint
{
    public class SessionClient : IDisposable
    {
        public string ConnectionId { get; private set; }
        public string Name { get; private set; }

        public bool Connected { get; set; }

        public SessionClient(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
    }
}