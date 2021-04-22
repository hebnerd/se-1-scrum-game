using Xunit;
using Bunit;
using System;
using ScrumGame.Shared.Cards;
using ScrumGame.Shared.Players;
using Bunit.Extensions.WaitForHelpers;
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

        [Fact]
        public void HandleSMFreePlayTest()
        {
            using var context = new TestContext();
            var indexComponent = context.RenderComponent<Index>();

            // Start the game
            indexComponent.Find("#start-game-button").Click();

            // Draw a card
            indexComponent.Find("#draw-card-button").Click();

            // Draw a card
            indexComponent.Find("#draw-card-button").Click();

            // Change story button should exist, otherwise test fails
            indexComponent.Find("#propose-story-change-button").Click();

            // Wait for modal to appear, then click yes to approve change for both SM and PO
            new WaitForAssertionHelper(indexComponent, () => indexComponent.Find(".btn-primary").Click(), TimeSpan.FromSeconds(3d));
            new WaitForAssertionHelper(indexComponent, () => indexComponent.Find(".btn-primary").Click(), TimeSpan.FromSeconds(3d));

            // Ensure all cards in scrum masters hand are being displayed
            foreach (var card in indexComponent.Instance.ActivePlayer.Hand)
            {
                Assert.Contains(card, indexComponent.Instance.ActivePlayer.Hand);
            }

            // Play it
            indexComponent.Find("#play-button").Click();

            // Ensure all cards have been removed after playing
            Assert.Empty(indexComponent.Instance.PlayCardContainer.Cards);
        }
    }
}
