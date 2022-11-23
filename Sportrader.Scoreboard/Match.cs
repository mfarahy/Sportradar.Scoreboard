using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportrader.Scoreboard
{
    public class Match
    {
        public event EventHandler OnCanceled;
        public event EventHandler OnFinished;
        public event EventHandler OnScoreUpdated;

        public
                public Match()
        {
        }

        public System.DateTime StartTime
        {
            get => default;
            set
            {
            }
        }

        public MatchStates Status
        {
            get => default;
            set
            {
            }
        }

        public string Description
        {
            get => default;
            set
            {
            }
        }

        public Team HomeTeam
        {
            get => default;
            set
            {
            }
        }

        public Team AwayTeam
        {
            get => default;
            set
            {
            }
        }

        public MatchResult? Result
        {
            get => default;
            set
            {
            }
        }

        public ushort HomeTeamScore
        {
            get => default;
            set
            {
            }
        }

        public ushort AwayTeamScore
        {
            get => default;
            set
            {
            }
        }

        public ushort TotalScore
        {
            get => default;
            set
            {
            }
        }

        public FluentResults.Result<MatchResult> Finish()
        {
            throw new NotImplementedException();
        }

        public FluentResults.Result<MatchResult> Cancel(CancelationReasons reason, string description)
        {
            throw new NotImplementedException();
        }

        public void UpdateScore(ushort homeTeamScore, ushort awayTeamScore)
        {
            throw new System.NotImplementedException();
        }
    }
}
