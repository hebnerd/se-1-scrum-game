using ScrumGame.Shared.Players;

namespace ScrumGame.Shared.Cards
{
    /// <summary>
    /// Implementation cards can be drawn by the developer. They increase 
    /// the progress towards completeing a story.
    /// </summary>
    public class ImplementationCard : Card
    {
        /// <summary>
        /// The amount of progress points gained when this card is played.
        /// </summary>
        public int ProgressPointsGained { get; set; }

        /// <summary>
        /// Creates a new implementation card. 
        /// </summary>
        /// <param name="description">The descriptive text of the card.</param>
        /// <param name="progressPointsGained">The progress points gained when played.</param>
        public ImplementationCard(string description, int progressPointsGained) : base(description)
        {
            ProgressPointsGained = progressPointsGained;
        }

        /// <summary>
        /// When an implementation card is played, it increases the progress 
        /// points towards the current story.
        /// </summary>
        /// <param name="player">The player who played the card.</param>
        public override void Play(Player player)
        {
            player.Team.Backlog.CurrentProgressPoints += ProgressPointsGained;
        }
    }
}
