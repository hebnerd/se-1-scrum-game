using System;
using ScrumGame.Shared.Players;

namespace ScrumGame.Shared.Cards
{
    /// <summary>
    /// Event cards can be randomly drawn by a dev. Event cards always have
    /// good effects when they are played.
    /// </summary>
    public class EventCard : Card
    {
        /// <summary>
        /// A function that gets run when this card is played. Should do 
        /// something good for the player or team who plays it.
        /// </summary>
        private readonly Action<Player> _onPlayed;
        
        /// <summary>
        /// Creates a new event card. The OnPlayed function gets called when a 
        /// player plays this card, and it should do something good when they 
        /// play it.
        /// </summary>
        /// <param name="description">The text that describes this card.</param>
        /// <param name="onPlayed">Something that happens when this card is played.</param>
        public EventCard(string description, Action<Player> onPlayed) : base(description)
        {
            _onPlayed = onPlayed;
        }

        /// <summary>
        /// When an event card is played, it runs the on played function, 
        /// which should do something good.
        /// </summary>
        /// <param name="player">The player who played the card.</param>
        public override void Play(Player player)
        {
            _onPlayed(player);
        }
    }
}
