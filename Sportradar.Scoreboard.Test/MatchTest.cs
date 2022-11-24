namespace Sportradar.Scoreboard.Test
{
    public class MatchTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StartShouldRunOnce()
        {
            Match match = new Match(new Team(1, "team1"), new Team(2, "team2"));

            var firstResult = match.Start();
            var secondResult = match.Start();

            Assert.That(firstResult.IsSuccess, Is.True);
            Assert.That(secondResult.IsSuccess, Is.False);
            Assert.That(match.Status, Is.EqualTo(MatchStates.Started));
        }

        [Test]
        public void FinishShouldRunOnce()
        {
            Match match = new Match(new Team(1, "team1"), new Team(2, "team2"));
            bool wasCalled = false;
            match.OnFinished += (object? sender, CompletedMatchResult e) => wasCalled = true;

            var finishBeforeStart = match.Finish();

            var startResult = match.Start();

            var finish = match.Finish();

            var secondFinish = match.Finish();

            Assert.That(startResult.IsSuccess, Is.True);
            Assert.That(wasCalled, Is.True);
            Assert.That(finish.IsSuccess, Is.True);
            Assert.That(finish.Value.Duration.TotalSeconds, Is.Positive);
            Assert.That(secondFinish.IsSuccess, Is.False);
            Assert.That(finishBeforeStart.IsSuccess, Is.False);
        }


        [Test]
        public void CancelShouldRunOnce()
        {
            const string STORM = "STORM";

            Match match = new Match(new Team(1, "team1"), new Team(2, "team2"));
            bool wasCalled = false;
            match.OnCanceled += (object? sender, CanceledMatchResult e) => wasCalled = true;

            var cancelBeforeStart = match.Cancel(CancelationReasons.NatualDisaster, STORM);

            var startResult = match.Start();

            var cancel = match.Cancel(CancelationReasons.NatualDisaster, STORM);

            var secondCancel = match.Cancel(CancelationReasons.NatualDisaster, STORM);

            Assert.That(startResult.IsSuccess, Is.True);
            Assert.That(wasCalled, Is.True);
            Assert.That(cancel.IsSuccess, Is.True);
            Assert.That(cancel.Value.Note, Is.EqualTo(STORM));
            Assert.That(cancel.Value.CancelationReason, Is.EqualTo(CancelationReasons.NatualDisaster));
            Assert.That(secondCancel.IsSuccess, Is.False);
            Assert.That(cancelBeforeStart.IsSuccess, Is.False);
        }

        [Test]
        public void UpdateShouldWorkFine()
        {
            Match match = new Match(new Team(1, "team1"), new Team(2, "team2"));
            bool wasCalled = false;
            match.OnScoreUpdated += (object? sender, Match e) => wasCalled = true;

            var updateBeforeStart = match.UpdateScore(1, 1);

            var startResult = match.Start();

            var secondUpdate = match.UpdateScore(2, 3);

            var thirdUpdate = match.UpdateScore(2, 3);

            match.Finish();

            var fourthUpdate = match.UpdateScore(4, 5);

            Assert.That(startResult.IsSuccess, Is.True);
            Assert.That(wasCalled, Is.True);
            Assert.That(secondUpdate.IsSuccess, Is.True);
            Assert.That(thirdUpdate.IsSuccess, Is.True);
            Assert.That(match.HomeTeamScore, Is.EqualTo(2));
            Assert.That(match.AwayTeamScore, Is.EqualTo(3));
            Assert.That(match.LastUpdate, Is.GreaterThan(match.StartTime));
            Assert.That(updateBeforeStart.IsSuccess, Is.False);
            Assert.That(fourthUpdate.IsSuccess, Is.False);
        }

        [Test]
        public void CheckEquality()
        {
            Match match1 = new Match(new Team(1, "team1"), new Team(2, "team2"));
            Match match2 = new Match(new Team(2, "team2"), new Team(1, "team1"));
            Match match3 = new Match(new Team(3, "team3"), new Team(4, "team4"));

            Assert.That(match1,Is.EqualTo(match2));
            Assert.That(match2, Is.Not.EqualTo(match3));
        }
    }
}