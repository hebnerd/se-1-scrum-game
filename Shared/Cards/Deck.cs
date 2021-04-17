using System;
using System.Collections.Generic;

namespace ScrumGame.Shared.Cards
{
    /// <summary>
    /// Represents a deck of cards that can be shuffled and drawn from.
    /// </summary>
    /// <typeparam name="T">The type of card that this deck is composed 
    /// of.</typeparam>
    public class Deck<T> where T : Card
    {
        /// <summary>
        /// All the cards in this deck.
        /// </summary>
        public readonly List<T> Cards = new List<T>();

        /// <summary>
        /// Create a new deck of cards. You may specify which cards should 
        /// start in the deck.
        /// </summary>
        /// <param name="cards">The cards that will be in this deck by 
        /// default.</param>
        public Deck(params T[] cards)
        {
            foreach (var card in cards)
            {
                Cards.Add(card);
            }
            ChangeCountCardText();

            Shuffle();
        }        

        /// <summary>
        /// Shuffles the entire deck.
        /// </summary>
        public void Shuffle()
        {
            var random = new Random();

            for (var i = 1; i < Cards.Count; i++)
            {
                var randomIndex = random.Next(Cards.Count - 1) + 1;

                var currentCard = Cards[i];
                var randomCard = Cards[randomIndex];

                Cards[i] = randomCard;
                Cards[randomIndex] = currentCard;
            }
        }

        /// <summary>
        /// Draws the first card off the top of the deck. Removes it from the deck. if 
        /// the deck is empty, returns null.
        /// </summary>
        /// <returns>The first card on the deck.</returns>
        public T DrawCard()
        {
            if (Cards.Count > 1)
            {
                var card = Cards[Cards.Count - 1];
                Cards.Remove(card);
                ChangeCountCardText();
                return card;
            }

            return null;
        }

        /// <summary>
        /// Adds a card to the deck. The card is added at the bottom, but the 
        /// deck is shuffled afterwords.
        /// </summary>
        /// <param name="card">The card to be added to the deck.</param>
        public void AddCard(T card)
        {
            Cards.Add(card);
            Shuffle();
        }

        public void ChangeCountCardText()
        {
            string text = "There are ";
            text += Cards.Count - 1;
            text += " Cards in the Deck";
            Cards[0].Description = text;
        }
    }
}
