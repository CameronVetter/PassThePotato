using System;
using System.Collections.Generic;

namespace PassThePotato.MessageContracts
{
    public interface ICurrentUsersInOrder
    {
        IList<Tuple<string, string>> Users { get; }
    }
}
