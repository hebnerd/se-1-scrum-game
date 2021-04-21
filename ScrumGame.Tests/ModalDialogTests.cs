using Xunit;
using Bunit;
using System;
using ScrumGame.Shared.Players;
using ScrumGame.Client.Components;
using Bunit.Extensions.WaitForHelpers;
using Index = ScrumGame.Client.Pages.Index;

namespace ScrumGame.Tests
{
    public class ModalDialogTests
    {
        /// <summary>
        /// Tests to make sure that cards get removed from the play card 
        /// container upon being played.
        /// </summary>
        [Fact]
        public void TestModalApproved()
        {
            using var context = new TestContext();

            var page = context.RenderComponent<Index>();
            var cc = context.RenderComponent<CardContainer>();

            page.Instance.PlayCardContainer = cc.Instance;

            page.Find("#start-game-button").Click();

            while (page.Instance.ActivePlayer is ProductOwner || page.Instance.ActivePlayer is ScrumMaster)
            {
                page.Find("#draw-card-button").Click();
            }

            // The next player should be a dev
            Assert.True(page.Instance.ActivePlayer is Developer);

            // If they click the change story button
            page.Find("#propose-story-change-button").Click();

            // It presents the modal to the SM
            new WaitForAssertionHelper(page, () => page.Find(".modal-title").MarkupMatches("<h5 class=\"modal-title\">Attention Scrum Master!</h5>"), TimeSpan.FromSeconds(3d));
            
            // Who presses yes
            new WaitForAssertionHelper(page, () => page.Find(".btn-primary").Click(), TimeSpan.FromSeconds(3d));

            // It then presents the modal to the PO
            new WaitForAssertionHelper(page, () => page.Find(".modal-title").MarkupMatches("<h5 class=\"modal-title\">Attention Product Owner!</h5>"), TimeSpan.FromSeconds(3d));

            // Who also presses yes
            new WaitForAssertionHelper(page, () => page.Find(".btn-primary").Click(), TimeSpan.FromSeconds(3d));           
        }
    }
}
