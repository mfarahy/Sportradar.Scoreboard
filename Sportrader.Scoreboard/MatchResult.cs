namespace Sportrader.Scoreboard
{
    public abstract class MatchResult
    {
        public MatchResult(Team homeTeam, Team awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public Team HomeTeam { private set; get; }

        public Team AwayTeam { private set; get; }
    }
}
