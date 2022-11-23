using FluentResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportrader.Scoreboard
{
    public class Scoreboard
    {
        public Scoreboard()
        {
            _onlineMatches = new HashSet<Match>();
        }

        private HashSet<Match> _onlineMatches;

        public IReadOnlyCollection<Match> OnlineMatches
        {
            get { return _onlineMatches; }
        }

        public Result<Match> CreateMatch(Team homeTeam, Team awayTeam)
        {
            if (_onlineMatches.Any(x => x.HomeTeam == homeTeam || x.AwayTeam == homeTeam))
            {
                return Result.Fail($"Currently ${homeTeam} is playing!");
            }
            if (_onlineMatches.Any(x => x.HomeTeam == awayTeam || x.AwayTeam == awayTeam))
            {
                return Result.Fail($"Currently ${awayTeam} is playing!");
            }
            if (homeTeam == awayTeam)
            {
                return Result.Fail($"A team can not play with itself!");
            }

            var match = new Match(homeTeam, awayTeam);
            match.OnCanceled += Match_OnCanceled;
            match.OnFinished += Match_OnFinished;

            var startResult = match.Start();
            if (!startResult.IsSuccess)
                return startResult;

            var updateResult = match.UpdateScore(0, 0);
            if (!updateResult.IsSuccess)
                return updateResult;

            _onlineMatches.Add(match);

            return Result.Ok(match);
        }

        private void Match_OnFinished(object? sender, Match e)
        {
            throw new NotImplementedException();
        }

        private void Match_OnCanceled(object? sender, Match e)
        {
            throw new NotImplementedException();
        }

        public ScoreboardSummary GetSummary()
        {
            throw new System.NotImplementedException();
        }
    }
}
