using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportrader.Scoreboard
{
    public class Team : IEquatable<Team>
    {
        public Team(int id,string name, string flag)
        {
            Id = id;
            Name = name;
            Flag = flag;
        }

        public int Id { get; }
        public string Name { get; }
        public string Flag { get; }

      

        public bool Equals(Team? other)
        {
            if (other == null) return false;
            return this.Id == other.Id;
        }
    }
}
