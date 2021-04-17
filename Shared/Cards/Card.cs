using ScrumGame.Shared.Players;

namespace ScrumGame.Shared.Cards
{
    /// <summary>
    /// Abstract base class for cards. Cards are stored in a deck and 
    /// given out to players, where they stay in their hand. Players 
    /// can play cards that exist in their hand.
    /// </summary>
    public abstract class Card
    {
        /// <summary>
        /// Text that describes what this card does.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Called when this card gets played.
        /// </summary>
        /// <param name="player">The player that played the card.</param>
        public abstract void Play(Player player);

        /// <summary>
        /// Specify the description for a card. This is the text that is shown 
        /// to the player that tells them what the card does.
        /// </summary>
        /// <param name="description">The descriptive text for the card.</param>
        protected Card(string description)
        {
            Description = description;
        }
    }
}
