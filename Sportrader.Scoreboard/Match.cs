using FluentResults;

namespace Sportrader.Scoreboard
{
    public class Match
    {
        public event EventHandler<CanceledMatchResult> OnCanceled;
        public event EventHandler<CompletedMatchResult> OnFinished;
        public event EventHandler<Match> OnScoreUpdated;

        public Match(Team homeTeam, Team awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public DateTime StartTime { get; set; } = default;
        public DateTime EndTime { get; set; } = default;

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

        public Result<CompletedMatchResult> Finish()
        {
            if (Status != MatchStates.Started)
            {
                return Result.Fail("A not started match is not able to get end!");
            }

            EndTime= DateTime.Now;
            var duration = EndTime - this.StartTime;
            var result = new CompletedMatchResult(HomeTeam, AwayTeam, HomeTeamScore, AwayTeamScore, duration);

            Status = MatchStates.Ended;

            OnFinished?.Invoke(this, result);

            return Result.Ok(result);
        }

        public Result<CanceledMatchResult> Cancel(CancelationReasons reason, string note)
        {
            if (Status != MatchStates.Started)
            {
                return Result.Fail("A not started match is not able to get canceled!");
            }

            var result = new CanceledMatchResult(HomeTeam, AwayTeam, reason, note);

            EndTime = DateTime.Now;
            Status = MatchStates.Canceled;   

            OnCanceled?.Invoke(this, result);

            return Result.Ok(result);
        }

        public Result UpdateScore(ushort homeTeamScore, ushort awayTeamScore)
        {
            if (Status != MatchStates.Started)
            {
                return Result.Fail("Score could get updated just after starting a match.");
            }

            HomeTeamScore = homeTeamScore;
            AwayTeamScore = awayTeamScore;

            LastUpdate= DateTime.Now;

            OnScoreUpdated?.Invoke(this, this);

            return Result.Ok();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Match another && ((another.HomeTeam.Equals(HomeTeam) && another.AwayTeam.Equals(AwayTeam)) || (
                another.AwayTeam.Equals(HomeTeam) && another.HomeTeam.Equals(AwayTeam))))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(HomeTeam, AwayTeam);
        }

        public override string ToString()
        {
            return $"{HomeTeam.Name}({HomeTeamScore}) vs {AwayTeam.Name}({AwayTeamScore})";
        }

        public bool IsParticipated(Team team)
        {
            return HomeTeam.Equals(team) || AwayTeam.Equals(team);
        }
    }
}
