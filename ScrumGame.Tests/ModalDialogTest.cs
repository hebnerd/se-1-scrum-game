using Xunit;
using Bunit;
using ScrumGame.Shared.Cards;
using ScrumGame.Client.Components;
using ScrumGame.Shared.Players;
using Index = ScrumGame.Client.Pages.Index;
using System;

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

            Assert.True(page.Instance.ActivePlayer is Developer);

            page.Find("#story-change-button").Click();
                       

            //page.Find(".btn-primary").Click();

            //page.Find(".modal-title").MarkupMatches("<h5 class=\"modal-title\">Attention Scrum Master!</h5>");



            //page.Find(".btn-primary").Click();

            //page.Find(".modal-title").MarkupMatches("<h5 class=\"modal-title\">Attention Scrum Master!</h5>");
        }
    }
}
