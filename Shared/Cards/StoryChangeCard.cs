using ScrumGame.Shared.Players;

namespace ScrumGame.Shared.Cards
{
    /// <summary>
    /// A change story card can be drawn by scrum masters. It will effect 
    /// the current story. Might have mixed results.
    /// </summary>
    public class StoryChangeCard : PlanningCard
    {
        /// <summary>
        /// Creates a new change story card. Alters a story by changing its 
        /// text and then causes some effect.
        /// </summary>
        /// <param name="description">The descriptive text of the card.</param>
        /// <param name="devPower">The change to all of the developers' power.</param>
        /// <param name="progressPoints">The change in progress points.</param>
        /// <param name="requiredProgressPoints">The required progress points.</param>
        public StoryChangeCard(string description, int devPower, int progressPoints, int requiredProgressPoints) : base(description, devPower, progressPoints, requiredProgressPoints)
        {
            // ...
        }

        /// <summary>
        /// When played, a change story card alter all the devs' power, the 
        /// progress points, and the needed progress points. It also changes
        /// the description of the first valid story, although this is purely
        /// visual.
        /// </summary>
        /// <param name="player">The player who played the card.</param>
        public override void Play(Player player)
        {
            base.Play(player);

            foreach (var story in player.Team.Backlog.Stories)
            {
                if (!string.IsNullOrEmpty(story.AltDescription) && story.Description != story.AltDescription)
                {
                    story.Description = story.AltDescription;
                }
            }
        }
    }
}
