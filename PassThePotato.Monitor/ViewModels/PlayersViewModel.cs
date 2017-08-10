using System.Collections.Generic;

namespace PassThePotato.Monitor.ViewModels
{
    public class PlayersViewModel 
    {
        public List<Player> Players { get; set; }
        
        public Potato Potato { get; set; }

        public PlayersViewModel()
        {
            Players = new List<Player>();
            Potato = new Potato();
        }

    }

    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasPotato { get; set; }
    }

    public class Potato
    {
        public string HolderId { get; set; }
        public string HolderName { get; set; }
    }
}
