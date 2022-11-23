using System.Text;

namespace Sportrader.Scoreboard
{
    public class ScoreboardSummary
    {
        public ScoreboardSummary(IEnumerable<Match> matches)
        {
            Matches = matches;
            SnapshotTime = DateTime.Now;
        }

        public DateTime SnapshotTime { get; private set; }
        public IEnumerable<Match> Matches { get; private set; }

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
