using System;
using System.Collections.Generic;
using System.Linq;
using PassThePotato.Monitor.ViewModels;

namespace PassThePotato.Monitor.Converters
{
    public static class TupleToPlayerConverter
    {
        public static List<Player> Convert(List<Tuple<string, string>> value)
        {

            //var realPlayers = value?.Select(p => new Player { Id = p.Item1, Name = p.Item2 }).ToList();

            //while (realPlayers.Count > 0 && realPlayers.Count < 50)
            //    realPlayers.AddRange(realPlayers);
            
            //return realPlayers;

            return value?.Select(p => new Player{ Id = p.Item1, Name = p.Item2}).ToList();
        }

        public static List<Tuple<string, string>> ConvertBack(List<Player> value)
        {
            return value?.Select(p => new Tuple<string,string>(p.Id, p.Name)).ToList();
        }
    }
}
