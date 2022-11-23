using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportrader.Scoreboard
{
    public class Scoreboard
    {
        public Scoreboard() { }

        public IReadOnlyList<Match> OnlineMatches
        {
            get => default;
            set
            {
            }
        }

        public void CreateMatch(Team homeTeam, Team awayTeam)
        {
            throw new System.NotImplementedException();
        }

        public void GetSummary()
        {
            throw new System.NotImplementedException();
        }
    }
}
