using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWLauncher
{

    public class GameInfo
    {
        public string name { get; set; }
        public int skin { get; set; }
        public string champion { get; set; }
        public int ribbon { get; set; }
        public string summoner1 { get; set; }
        public string summoner2 { get; set; }
        public string rank { get; set; }
        public string team { get; set; }
        public int icon { get; set; }
    }

    public class GameInfo2 {
        public string MANACOSTS_ENABLED { get; set; }
        public string COOLDOWNS_ENABLED { get; set; }
        public string CHEATS_ENABLED { get; set; }
        public string MINION_SPAWNS_ENABLED { get; set; }

    }
}
