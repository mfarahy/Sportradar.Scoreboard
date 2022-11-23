using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportrader.Scoreboard
{
    public class Match
    {
        public event EventHandler<Match> OnCanceled;
        public event EventHandler<Match> OnFinished;
        public event EventHandler<Match> OnScoreUpdated;

        public Match(Team homeTeam, Team awayTeam)
        {
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
        }

        public DateTime StartTime { get; set; } = default;

        public DateTime LastUpdate { get; set; } = default;

        public MatchStates Status { get; set; } = MatchStates.None;

        public string Description { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public CompletedMatchResult? MatchResult { get; set; } = null;

        public ushort HomeTeamScore { get; set; } = 0;

        public ushort AwayTeamScore { get; set; } = 0;

        public ushort TotalScore
        {
            get { return (ushort)(HomeTeamScore + AwayTeamScore); }
        }

        public Result Start()
        {
            if (Status != MatchStates.None)
            {
                return Result.Fail("Invalid status. Create another match to start.");
            }


            StartTime = DateTime.Now;
            Status = MatchStates.Started;

            return Result.Ok();
        }

        public Result<MatchResult> Finish()
        {
            if (Status != MatchStates.Started)
            {
                return Result.Fail("A not started match is not able to get end!");
            }

            TimeSpan duration = (TimeSpan)(DateTime.Now - this.StartTime);
            MatchResult result = new CompletedMatchResult(HomeTeam, AwayTeam, HomeTeamScore, AwayTeamScore, duration);
           
            OnFinished?.Invoke(this, this);

            return Result.Ok(result);
        }

        public Result<CompletedMatchResult> Cancel(CancelationReasons reason, string note)
        {
            throw new NotImplementedException();
        }

        public Result UpdateScore(ushort homeTeamScore, ushort awayTeamScore)
        {
            if (Status != MatchStates.Started)
                return Result.Fail("Score could get updated just after starting a match.");

            this.HomeTeamScore = homeTeamScore;
            this.AwayTeamScore = awayTeamScore;

            return Result.Ok();
        }
    }
}
