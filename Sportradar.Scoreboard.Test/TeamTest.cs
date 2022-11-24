namespace Sportradar.Scoreboard.Test
{
    public class TeamTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldNotBeEqualByDifferentNames()
        {
            Team team1 = new Team(1, "team1");
            Team team2 = new Team(2, "team2");

            Assert.That(team2, Is.Not.EqualTo(team1));
        }

        [Test]
        public void ShouldNotBeEqualBySameNameButDifferentIds()
        {
            Team team1 = new Team(1, "team1");
            Team team2 = new Team(2, "team1");

            Assert.That(team2, Is.Not.EqualTo(team1));
        }


        [Test]
        public void ShouldBeEqualBySameIds()
        {
            Team team1 = new Team(1, "team1");
            Team team2 = new Team(1, "team2");

            Assert.That(team2, Is.EqualTo(team1));
        }
    }
}