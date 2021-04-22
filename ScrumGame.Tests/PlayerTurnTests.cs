using Xunit;
using Bunit;
using System;
using ScrumGame.Shared;
using ScrumGame.Shared.Players;
using Bunit.Extensions.WaitForHelpers;
using Index = ScrumGame.Client.Pages.Index;

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
            using var context = new TestContext();
            var indexComponent = context.RenderComponent<Index>();

            indexComponent.Find("#start-game-button").Click();

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 1), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();
            
            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 2), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 0), TimeSpan.FromSeconds(3d));
        }

        /// <summary>
        /// Tests for player turns with two developers (four players).
        /// </summary>
        [Fact]
        public void Test2Devs()
        {
            using var context = new TestContext();
            var indexComponent = context.RenderComponent<Index>();

            indexComponent.Find("#increase-devs-button").Click();

            indexComponent.Find("#start-game-button").Click();

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 1), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 2), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 3), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 0), TimeSpan.FromSeconds(3d));
        }

        /// <summary>
        /// Tests for player turns with three developers (five players).
        /// </summary>
        [Fact]
        public void Test3Devs()
        {
            using var context = new TestContext();
            var indexComponent = context.RenderComponent<Index>();

            indexComponent.Find("#increase-devs-button").Click();

            indexComponent.Find("#increase-devs-button").Click();

            indexComponent.Find("#start-game-button").Click();

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 1), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 2), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 3), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 4), TimeSpan.FromSeconds(3d));

            indexComponent.Find("#skip-turn-button").Click();

            new WaitForAssertionHelper(indexComponent, () => Assert.True(indexComponent.Instance.ActivePlayerIndex == 0), TimeSpan.FromSeconds(3d));
        }
    }
}
