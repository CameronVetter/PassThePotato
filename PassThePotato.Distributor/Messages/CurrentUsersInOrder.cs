using System;
using System.Collections.Generic;
using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor.Messages
{
    public class CurrentUsersInOrder : ICurrentUsersInOrder
    {
        public IList<Tuple<string, string>> Users { get; }
    }
}