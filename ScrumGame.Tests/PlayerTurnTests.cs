using Xunit;
using ScrumGame.Shared;
using ScrumGame.Shared.Players;

namespace ScrumGame.Tests
{
    public class PlayerTurnTests
    {
        /// <summary>
        /// Tests for player turns with one developer (three players).
        /// </summary>
        [Fact]
        public void Test1Dev()
        {
            var index = new ScrumGame.Client.Pages.Index();

            var productOwner = new ProductOwner("Test Product Owner");
            var scrumMaster = new ScrumMaster("Test Scrum Master");
            var developer = new Developer("Test Developer 1");

            index.GameManager = new GameManager(productOwner, scrumMaster, developer);

            index.NextTurn();

            Assert.True(index.ActivePlayer == productOwner);

            index.NextTurn();

            Assert.True(index.ActivePlayer == scrumMaster);

            index.NextTurn();

            Assert.True(index.ActivePlayer == developer);

            index.NextTurn();

            Assert.True(index.ActivePlayer == productOwner);
        }

        /// <summary>
        /// Tests for player turns with two developers (four players).
        /// </summary>
        [Fact]
        public void Test2Devs()
        {
            var index = new ScrumGame.Client.Pages.Index();

            var productOwner = new ProductOwner("Test Product Owner");
            var scrumMaster = new ScrumMaster("Test Scrum Master");
            var developer1 = new Developer("Test Developer 1");
            var developer2 = new Developer("Test Developer 2");

            index.GameManager = new GameManager(productOwner, scrumMaster, developer1, developer2);

            index.NextTurn();

            Assert.True(index.ActivePlayer == productOwner);

            index.NextTurn();

            Assert.True(index.ActivePlayer == scrumMaster);

            index.NextTurn();

            Assert.True(index.ActivePlayer == developer1);

            index.NextTurn();

            Assert.True(index.ActivePlayer == developer2);

            index.NextTurn();

            Assert.True(index.ActivePlayer == productOwner);
        }

        /// <summary>
        /// Tests for player turns with three developers (five players).
        /// </summary>
        [Fact]
        public void Test3Devs()
        {
            var index = new ScrumGame.Client.Pages.Index();

            var productOwner = new ProductOwner("Test Product Owner");
            var scrumMaster = new ScrumMaster("Test Scrum Master");
            var developer1 = new Developer("Test Developer 1");
            var developer2 = new Developer("Test Developer 2");
            var developer3 = new Developer("Test Developer 3");

            index.GameManager = new GameManager(productOwner, scrumMaster, developer1, developer2, developer3);

            index.NextTurn();

            Assert.True(index.ActivePlayer == productOwner);

            index.NextTurn();

            Assert.True(index.ActivePlayer == scrumMaster);

            index.NextTurn();

            Assert.True(index.ActivePlayer == developer1);

            index.NextTurn();

            Assert.True(index.ActivePlayer == developer2);

            index.NextTurn();

            Assert.True(index.ActivePlayer == developer3);

            index.NextTurn();

            Assert.True(index.ActivePlayer == productOwner);
        }
    }
}
