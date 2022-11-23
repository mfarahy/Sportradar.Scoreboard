using System.Text;

namespace Sportrader.Scoreboard
{
    public class ScoreboardSummary
    {
        public ScoreboardSummary(IOrderedEnumerable<Match> matches)
        {
            Matches = matches;
            SnapshotTime = DateTime.Now;
        }

        public DateTime SnapshotTime { get; private set; }
        public IOrderedEnumerable<Match> Matches { get; private set; }

        public override string ToString()
        {
            StringBuilder sb= new StringBuilder();
            if (Matches != null)
            {
                foreach (var match in Matches)
                {
                    sb.AppendLine(match.ToString());
                }
            }
            return sb.ToString();
        }
    }
}
