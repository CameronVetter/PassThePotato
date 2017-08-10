using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassThePotato.Distributor.Messages;
using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor
{
    public static class PotatoManager
    {
        public static Random Rnd = new Random(DateTime.Now.Millisecond);
        private static List<Tuple<string, string>> _users = new List<Tuple<string, string>>();

        public static Tuple<string, string> Holder;

        public static void UpdateUsers(List<Tuple<string, string>> users)
        {
            _users = users;
        }

        public static async Task CheckPotatoState()
        {
            lock (_users)
            {
                if (Holder != null)
                {
                    if (!_users.Contains(Holder))
                    {
                        Holder = null;
                    }
                }

                if (Holder == null)
                {
                    if (_users.Count > 0)
                    {
                        Holder = _users[Rnd.Next(0, _users.Count)];
                    }
                }
            }

            if (Holder == null)
            {
                await ServiceBus.Bus.Publish((IWhoHasThePotato)new WhoHasThePotato(string.Empty, string.Empty));
            }
            else
            {
                await ServiceBus.Bus.Publish((IWhoHasThePotato)new WhoHasThePotato(Holder.Item1, Holder.Item2));
            }
        }

        public static async Task WhoCanIPassTo(string id)
        {
            // they are no longer the holder for some reason
            if (Holder.Item1 != id) return;

            var message = new WhoCanYouPassToo
            {
                RequestorId = id
            };

            lock (_users)
            {
                int i;
                for (i = 0; i < _users.Count; i++)
                {
                    if (_users[i].Item1 == id)
                    {
                        break;
                    }
                }

                if (i == 0)
                {
                    message.LeftId = _users[_users.Count-1].Item1;
                    message.LeftName = _users[_users.Count-1].Item2;
                }
                else
                {
                    message.LeftId = _users[i-1].Item1;
                    message.LeftName = _users[i-1].Item2;
                }

                if (i == _users.Count-1)
                {
                    message.RightId = _users[0].Item1;
                    message.RightName = _users[0].Item2;
                }
                else
                {
                    message.RightId = _users[i + 1].Item1;
                    message.RightName = _users[i + 1].Item2;
                }
            }

            await ServiceBus.Bus.Publish((IWhoCanYouPassTo)message);
        }

        public static async Task PassPotato(string passerId, string receiverId)
        {
            if (passerId != Holder.Item1)
            {
                await ServiceBus.Bus.Publish((IError)new Error(passerId, "Player passing potato no longer has potato"));
            }

            var user = _users.FirstOrDefault(u => u.Item1 == receiverId);

            if (_users.All(u => u.Item1 != receiverId))
            {
                await ServiceBus.Bus.Publish((IError)new Error(passerId, "Player receiving potato no longer connectecd"));
            }
            else
            {
                Holder = user;
                await CheckPotatoState();
            }
        }
    }
}