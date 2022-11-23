using FluentResults;

namespace Sportrader.Scoreboard
{
    public class Match: IComparable<Match>
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

            var duration = DateTime.Now - this.StartTime;
            var result = new CompletedMatchResult(HomeTeam, AwayTeam, HomeTeamScore, AwayTeamScore, duration);

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

            OnScoreUpdated?.Invoke(this, this);

            return Result.Ok();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Match another && ((another.HomeTeam == HomeTeam && another.AwayTeam == AwayTeam) || (
                another.AwayTeam == HomeTeam && another.HomeTeam == AwayTeam)))
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
            return $"${HomeTeam.Name}(${HomeTeamScore}) vs ${AwayTeam.Name}({AwayTeamScore})";
        }

        public int CompareTo(Match? other)
        {
            if (other == null) return -1;

            return other.TotalScore == TotalScore ?
                StartTime.CompareTo(other.StartTime):
                other.TotalScore.CompareTo(other.TotalScore);
        }
    }
}
