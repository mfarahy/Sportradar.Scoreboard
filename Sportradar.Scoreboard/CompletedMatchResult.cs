namespace Sportradar.Scoreboard
{
    public class CompletedMatchResult: MatchResult
    {
        public CompletedMatchResult(Team homeTeam, Team awayTeam, ushort homeTeamScore, ushort awayTeamScore, TimeSpan duration)
            :base( homeTeam,  awayTeam)
        {
            Duration = duration;
            HomeTeamScore = homeTeamScore;
            AwayTeamScore = awayTeamScore;
        }

        public TimeSpan Duration { private set; get; }

        public ushort? HomeTeamScore { private set; get; }

        public ushort? AwayTeamScore { private set; get; }
    }
}
