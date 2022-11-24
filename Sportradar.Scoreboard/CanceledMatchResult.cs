namespace Sportradar.Scoreboard
{
    public class CanceledMatchResult : MatchResult
    {
        public CanceledMatchResult(Team homeTeam, Team awayTeam, CancelationReasons cancelationReason, string note)
            : base(homeTeam, awayTeam)
        {
            CancelationReason = cancelationReason;
            Note = note;
        }

        public CancelationReasons CancelationReason { private set; get; }

        public string Note { private set; get; }
    }
}
