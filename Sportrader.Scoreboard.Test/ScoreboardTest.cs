using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportrader.Scoreboard.Test
{
    public class ScoreboardTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckCreateMatch()
        {
            Scoreboard scoreboard = new Scoreboard();

            var sameTeam = scoreboard.CreateMatch(new Team(1, "team1"), new Team(1, "team1"));
            var homeTeamNull = scoreboard.CreateMatch(null, new Team(1, "team1"));
            var awayTeamNull = scoreboard.CreateMatch(new Team(1, "team1"), null);
            var successCreation = scoreboard.CreateMatch(new Team(1, "team1"), new Team(2, "team2"));
            var firstRepetitive = scoreboard.CreateMatch(new Team(1, "team1"), new Team(2, "team2"));
            var secondRepetitive = scoreboard.CreateMatch(new Team(2, "team2"), new Team(1, "team1"));
            var homeRepetitive = scoreboard.CreateMatch(new Team(1, "team1"), new Team(3, "team3"));
            var awayRepetitive = scoreboard.CreateMatch(new Team(3, "team3"), new Team(2, "team2"));


            Assert.That(sameTeam.IsSuccess, Is.False);
            Assert.That(homeTeamNull.IsSuccess, Is.False);
            Assert.That(awayTeamNull.IsSuccess, Is.False);
            Assert.That(successCreation.IsSuccess, Is.True);
            Assert.That(firstRepetitive.IsSuccess, Is.False);
            Assert.That(secondRepetitive.IsSuccess, Is.False);
            Assert.That(homeRepetitive.IsSuccess, Is.False);
            Assert.That(awayRepetitive.IsSuccess, Is.False);
        }

        [Test]
        public void CheckFinishMatch()
        {
            Scoreboard scoreboard = new Scoreboard();

            var creationResult = scoreboard.CreateMatch(new Team(1, "team1"), new Team(2, "team2"));
            var match = creationResult.Value;
            bool isMatchOnBoard = scoreboard.OnlineMatches.Any(x => x.Equals(match));
            match.Finish();
            bool isMatchRemainOnBoardAfterFinishing = scoreboard.OnlineMatches.Any(x => x.Equals(match));

            Assert.That(creationResult.IsSuccess, Is.True);
            Assert.That(isMatchOnBoard, Is.True);
            Assert.That(isMatchRemainOnBoardAfterFinishing, Is.False);
        }

        [Test]
        public void CheckCancelMatch()
        {
            Scoreboard scoreboard = new Scoreboard();

            var creationResult = scoreboard.CreateMatch(new Team(1, "team1"), new Team(2, "team2"));
            var match = creationResult.Value;
            bool isMatchOnBoard = scoreboard.OnlineMatches.Any(x => x.Equals(match));
            match.Cancel( CancelationReasons.NatualDisaster,"Not important");
            bool isMatchRemainOnBoardAfterCanceling = scoreboard.OnlineMatches.Any(x => x.Equals(match));

            Assert.That(creationResult.IsSuccess, Is.True);
            Assert.That(isMatchOnBoard, Is.True);
            Assert.That(isMatchRemainOnBoardAfterCanceling, Is.False);
        }

        [Test]
        public void CheckGetSummary()
        {
            Scoreboard scoreboard = new Scoreboard();

            var MexicoCanada = scoreboard.CreateMatch(new Team(1, "Mexico"), new Team(2, "Canada"));
            MexicoCanada.Value.UpdateScore(0, 5);
            var SpainBrazil = scoreboard.CreateMatch(new Team(3, "Spain"), new Team(4, "Brazil"));
            SpainBrazil.Value.UpdateScore(10, 2);
            var GermanyFrance = scoreboard.CreateMatch(new Team(5, "Germany"), new Team(6, "France"));
            GermanyFrance.Value.UpdateScore(2, 2);
            var UruguayItaly = scoreboard.CreateMatch(new Team(7, "Uruguay"), new Team(8, "Italy"));
            UruguayItaly.Value.UpdateScore(6, 6);
            var ArgentinaAustralia = scoreboard.CreateMatch(new Team(9, "Argentina"), new Team(10, "Australia"));
            ArgentinaAustralia.Value.UpdateScore(3, 1);
            var summary= scoreboard.GetSummary();
            var summaryArray = summary.Matches.ToArray();

            Assert.That(MexicoCanada.IsSuccess, Is.True);
            Assert.That(SpainBrazil.IsSuccess, Is.True);
            Assert.That(GermanyFrance.IsSuccess, Is.True);
            Assert.That(UruguayItaly.IsSuccess, Is.True);
            Assert.That(ArgentinaAustralia.IsSuccess, Is.True);
            Assert.That(summaryArray[0], Is.EqualTo(UruguayItaly.Value));
            Assert.That(summaryArray[1], Is.EqualTo(SpainBrazil.Value));
            Assert.That(summaryArray[2], Is.EqualTo(MexicoCanada.Value));
            Assert.That(summaryArray[3], Is.EqualTo(ArgentinaAustralia.Value));
            Assert.That(summaryArray[4], Is.EqualTo(GermanyFrance.Value));
        }
    }
}
