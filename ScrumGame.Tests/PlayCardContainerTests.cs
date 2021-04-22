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

        /// <summary>
        /// Verifies that when a story change can be proposed by a developer,
        /// that when "HandleOnClickDevProposeChange" is executed, the modal
        /// dialog appears for the Scrum Master (assume the Scrum Master
        /// denies) and that it doesn't appear for the Product Owner.
        /// </summary>
        [Fact]
        public void HandleSMDisapproveTest()
        {
            using var context = new TestContext();
            var indexComponent = context.RenderComponent<Index>(); //Render the Index page

            // Start the game
            indexComponent.Find("#start-game-button").Click();

            // Draw a card for Product Owner
            indexComponent.Find("#draw-card-button").Click();

            // Draw a card for Scrum Master
            indexComponent.Find("#draw-card-button").Click();

            // Developer proposes a story change to the Scrum Master
            indexComponent.Find("#propose-story-change-button").Click();

            // Wait for modal to appear for the Scrum Master, click 'No' to deny the story change proposal
            new WaitForAssertionHelper(indexComponent, () => indexComponent.Find(".btn-secondary").Click(), TimeSpan.FromSeconds(3d));

            // Assert that active player is now the Product Owner
            Assert.True(indexComponent.Instance.ActivePlayer is ProductOwner);

            // Assert that the modal does not prompt the Product Owner after Scrum Master denies (proposalStatus == 1)
            Assert.Equal(1, indexComponent.Instance.proposalStatus);
        }
    }
}
