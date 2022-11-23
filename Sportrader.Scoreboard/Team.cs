using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportrader.Scoreboard
{
    public class Team
    {
        public Team(string name, string flag)
        {
            Name = name;
            Flag = flag;
        }

        public string Name { get; }
        public string Flag { get; }
    }
}
