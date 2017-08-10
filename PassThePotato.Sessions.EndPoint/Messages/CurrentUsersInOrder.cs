using System;
using System.Collections.Generic;
using PassThePotato.MessageContracts;

namespace PassThePotato.Sessions.EndPoint.Messages
{
    public class CurrentUsersInOrder : ICurrentUsersInOrder
    {
        public IList<Tuple<string, string>> Users { get; }

        public CurrentUsersInOrder()
        {
            Users = HubSessionManager.GetManager.GetUsersAsTuple();
        }
    }
}