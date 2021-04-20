using Xunit;
using Bunit;
using ScrumGame.Shared.Cards;
using ScrumGame.Client.Components;
using Index = ScrumGame.Client.Pages.Index;

namespace ScrumGame.Tests
{
    public class PlayCardContainerTests
    {
        /// <summary>
        /// Tests to make sure that cards get removed from the play card 
        /// container upon being played.
        /// </summary>
        [Fact]
        public void TestPlayCard()
        {
            using var context = new TestContext();

            var page = context.RenderComponent<Index>();
            var cc = context.RenderComponent<CardContainer>();

            page.Instance.PlayCardContainer = cc.Instance;

            page.Find("#start-game-button").Click();

            Assert.Empty(page.Instance.PlayCardContainer.Cards);

            // Add test card
            page.Instance.PlayCardContainer.AddCard(new DilemmaCard("Test Card", 1, 1, 1));

            // Card should be in hand
            Assert.NotEmpty(page.Instance.PlayCardContainer.Cards);

            // Click the play button, which plays the card
            page.Find("#play-button").Click();

            // The play card container should be empty again
            Assert.Empty(page.Instance.PlayCardContainer.Cards);
        }
    }
}
