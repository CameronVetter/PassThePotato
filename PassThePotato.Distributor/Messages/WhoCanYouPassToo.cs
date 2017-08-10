using System;
using PassThePotato.MessageContracts;

namespace PassThePotato.Distributor.Messages
{
    public class WhoCanYouPassToo : IWhoCanYouPassTo
    {
        public string LeftId { get; set; }

        public string LeftName { get; set; }

        public string RightId { get; set; }

        public string RightName { get; set; }

        public string RequestorId { get; set; }
    }
}