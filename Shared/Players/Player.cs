using ScrumGame.Shared.Cards;
using System.Collections.Generic;

namespace ScrumGame.Shared.Players
{
    /// <summary>
    /// Abstract base class representing a player.
    /// </summary>
    public abstract class Player
    {
        /// <summary>
        /// The player's name that they enter.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The team this player is on.
        /// </summary>
        public Team Team { get; set; }
        /// <summary>
        /// These are the cards available to the player for them to play each 
        /// turn.
        /// </summary>
        public readonly List<Card> Hand = new List<Card>();

        /// <summary>
        /// Creates a new player with the specified name. Specify what team 
        /// this player is on.
        /// </summary>
        /// <param name="name">The player's name.</param>
        /// <param name="team">The team they are on.</param>
        public Player(string name)
        {
            Name = name;
        }
    }
}
