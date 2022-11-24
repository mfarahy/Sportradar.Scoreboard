namespace Sportradar.Scoreboard
{
    internal class MatchComparer : IComparer<Match>
    {

        public int Compare(Match? x, Match? y)
        {
            if (x == null) return -1;

            if (y == null) return 1;

            return x.TotalScore == y.TotalScore ?
                x.StartTime.CompareTo(y.StartTime) :
                x.TotalScore.CompareTo(y.TotalScore);
        }
    }
}
