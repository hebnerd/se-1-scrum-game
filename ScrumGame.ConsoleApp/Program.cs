using System;
using ScrumGame.Shared;
using ScrumGame.Shared.Cards;
using ScrumGame.Shared.Players;

namespace ScrumGame.ConsoleApp
{
    public class Program
    {
        private static GameManager _gameManager;

        public static void Main(string[] args)
        {
            start:
            Console.WriteLine("Welcome to Scrum Game Sprint 2 Prototype!");
            Console.Write("Enter number of players (3 - 7): ");

            if (!int.TryParse(Console.ReadLine(), out var numberOfPlayers) || numberOfPlayers < 3 || numberOfPlayers > 7)
            {
                Console.WriteLine("Not a valid entry. Game only supports 3 to 7 players.");
                goto start;
            }

            var players = new Player[numberOfPlayers];

            Console.Write("\nEnter the name of the Product Owner: ");
            var productOwner = new ProductOwner(Console.ReadLine());
            players[0] = productOwner;

            Console.Write("\nEnter the name of the Scrum Master: ");
            var scrumMaster = new ScrumMaster(Console.ReadLine());
            players[1] = scrumMaster;

            var developers = new Developer[numberOfPlayers - 2];
            for (var i = 0; i < numberOfPlayers - 2; i++)
            {
                Console.Write($"\nEnter the name of Developer #{i + 1}: ");
                developers[i] = new Developer(Console.ReadLine());
                players[i + 2] = developers[i];
            }

            _gameManager = new GameManager(players);

            //
            //   MAIN GAME LOOP
            //
            int turn = 1;
            while (_gameManager.Team.Backlog.Stories.Count > 0)
            {
                Console.WriteLine($"\nTurn {turn++} begin!");
                foreach (var player in players)
                {
                    Console.WriteLine($"It's {player.Name}'s turn!");

                    if (player is ProductOwner p)
                    {
                        po_turn:
                        Console.WriteLine($"What would you like to do Product Owner?");
                        Console.WriteLine("\t1. Draw a Dilemma Card.");
                        Console.WriteLine("\t2. Play a card from my hand.");
                        Console.WriteLine("\t3. Set backlog priority.");

                        if (int.TryParse(Console.ReadLine(), out var result) || result < 1 || result > 2)
                        {
                            if (result == 1)
                            {
                                var dilemmaCard = _gameManager.DilemmaDeck.DrawCard();
                                p.Hand.Add(dilemmaCard);

                                Console.WriteLine(GetHandString(p));
                            }
                            else if (result == 2 && p.Hand.Count > 0)
                            {
                                GetAndPlayCard(p);
                            }
                            else if (result == 3)
                            {
                                change_priority:
                                Console.WriteLine("Here's the current backlog:\n");
                                Console.WriteLine(GetBacklogString(_gameManager.Team.Backlog));
                                Console.WriteLine("\nWhich story do you want to be the priortiy?");

                                if (int.TryParse(Console.ReadLine(), out var storyIndex) && storyIndex > 0 && storyIndex <= _gameManager.Team.Backlog.Stories.Count)
                                {
                                    var oldPriority = _gameManager.Team.Backlog.Stories[0];
                                    var newPriority = _gameManager.Team.Backlog.Stories[storyIndex - 1];

                                    _gameManager.Team.Backlog.Stories[0] = newPriority;
                                    _gameManager.Team.Backlog.Stories[storyIndex - 1] = oldPriority;
                                }
                                else
                                {
                                    goto change_priority;
                                }
                            }
                            else
                            {
                                goto po_turn;
                            }
                        }
                        else
                        {
                            goto po_turn;
                        }
                    }
                    else if (player is ScrumMaster s)
                    {
                        sm_turn:
                        Console.WriteLine($"What would you like to do Scrum Master?");
                        Console.WriteLine("\t1. Draw a Story Change Card.");
                        Console.WriteLine("\t2. Play a card from my hand.");

                        if (int.TryParse(Console.ReadLine(), out var result) || result < 1 || result > 2)
                        {
                            if (result == 1)
                            {
                                var storyChangeCard = _gameManager.StoryChangeDeck.DrawCard();
                                s.Hand.Add(storyChangeCard);

                                Console.WriteLine(GetHandString(s));
                            }
                            else if (result == 2 && s.Hand.Count > 0)
                            {
                                GetAndPlayCard(s);
                            }
                            else
                            {
                                goto sm_turn;
                            }
                        }
                        else
                        {
                            goto sm_turn;
                        }
                    }
                    else if (player is Developer d)
                    {
                        d_turn:
                        Console.WriteLine($"What would you like to do developer?");
                        Console.WriteLine("\t1. Draw an Implementation Card.");
                        Console.WriteLine("\t2. Draw an Event Card.");
                        Console.WriteLine("\t3. Play a card from my hand.");

                        if (int.TryParse(Console.ReadLine(), out var result) || result < 1 || result > 3)
                        {
                            if (result == 1)
                            {
                                var implementationCard = _gameManager.ImplementationDeck.DrawCard();
                                d.Hand.Add(implementationCard);

                                Console.WriteLine(GetHandString(d));
                            }
                            else if (result == 2)
                            {
                                var ceiling = 100 - 5 * d.DevPower;

                                var random = new Random();
                                var roll = random.Next(0, ceiling);

                                if (roll < 50)
                                {
                                    Console.WriteLine("Success! You get an event card!");
                                    var eventCard = _gameManager.EventCardDeck.DrawCard();
                                    d.Hand.Add(eventCard);

                                    Console.WriteLine(GetHandString(d));
                                }
                                else
                                {
                                    Console.WriteLine("Uh-oh... A bad event happened...");
                                    switch (random.Next(4))
                                    {
                                        case 0:
                                            Console.WriteLine("The devs' last unit test threw several exceptions. (-1 Progress)");
                                            _gameManager.Team.Backlog.CurrentProgressPoints -= 1;
                                            break;
                                        case 1:
                                            Console.WriteLine("You won't constribute much today. (-1 Dev Power to You)");
                                            d.DevPower -= 1;
                                            break;
                                        case 2:
                                            Console.WriteLine("Your developers were unsure how to implemnt some of the requirements. (-1 Dev Power To All)");
                                            foreach (var ply in _gameManager.Team.Players)
                                            {
                                                if (ply is Developer dev)
                                                {
                                                    dev.DevPower -= 1;
                                                }
                                            }

                                            break;
                                        case 3:
                                            Console.WriteLine("Turns out you really misunderstood a story, and it's going to take longer than expected. (+1 Required Progress Points)");
                                            _gameManager.Team.Backlog.RequiredProgressPoints += 1;

                                            break;

                                    }
                                }
                            }
                            else if (result == 3 && d.Hand.Count > 0)
                            {
                                GetAndPlayCard(d);
                            }
                            else
                            {
                                goto d_turn;
                            }
                        }
                        else
                        {
                            goto d_turn;
                        }
                    }
                }

                if (_gameManager.Team.Backlog.CurrentProgressPoints >= _gameManager.Team.Backlog.RequiredProgressPoints)
                {
                    _gameManager.Team.Backlog.Stories.RemoveAt(0);
                    _gameManager.Team.Backlog.CurrentProgressPoints = 0;
                    _gameManager.Team.Backlog.RequiredProgressPoints = 4;
                }

                Console.WriteLine("Here what your backlog looks like:");
                Console.WriteLine(GetBacklogString(_gameManager.Team.Backlog));
            }

            Console.WriteLine("\nAll stories finished! Congratulations!");
        }

        /// <summary>
        /// Prompts a player to pick a card from their deck, and plays it.
        /// </summary>
        /// <param name="p">The player to pick a card.</param>
        private static void GetAndPlayCard(Player p)
        {
            hand:
            Console.WriteLine(GetHandString(p));
            Console.WriteLine($"\nWhat card will you play?");
            if (int.TryParse(Console.ReadLine(), out var cardIndex) || cardIndex < 1 || cardIndex > p.Hand.Count)
            {
                var card = p.Hand[cardIndex - 1];

                if (card != null)
                {
                    card.Play(p);

                    // Remove the card from the player's hand
                    p.Hand.Remove(card);

                    // Put the card back in the appropriate deck for reuse
                    switch (card)
                    {
                        case DilemmaCard d:
                            _gameManager.DilemmaDeck.AddCard(d);
                            break;
                        case EventCard e:
                            _gameManager.EventCardDeck.AddCard(e);
                            break;
                        case ImplementationCard i:
                            _gameManager.ImplementationDeck.AddCard(i);
                            break;
                        case StoryChangeCard s:
                            _gameManager.StoryChangeDeck.AddCard(s);
                            break;
                        default:
                            throw new ArgumentException("Invalid card type in player's hand.");
                    }
                }
                else
                {
                    goto hand;
                }
            }
            else
            {
                goto hand;
            }
        }

        /// <summary>
        /// Gets a string formatted to display the player's current hand.
        /// </summary>
        /// <param name="player">The player whose hand to get.</param>
        /// <returns>A string formatted containg hand info.</returns>
        private static string GetHandString(Player player)
        {
            string s = "This is your hand:\n";
            for (var i = 0; i < player.Hand.Count; i++)
            {
                var card = player.Hand[i];

                s += $"{i + 1}. [{card.GetType().Name}] {card.Description}\n";
            }

            return s;
        }

        /// <summary>
        /// Get a string formatted to print the current backlog status.
        /// </summary>
        /// <param name="backlog">The backlog status to get.</param>
        /// <returns>A formatted string containing backlog info.</returns>
        private static string GetBacklogString(Backlog backlog)
        {
            var result = string.Empty;

            result += "\n===================================================\n";
            for (var i = 0; i < backlog.Stories.Count; i++)
            {
                Story story = backlog.Stories[i];
                result += $"{i + 1}. {story.Description} {(i == 0 ? "(CURRENT PRIORITY)" : string.Empty)}\n";
            }

            result += "===================================================\n\n";

            result += "Progress Towards Current Story:\n";
            result += $"{backlog.CurrentProgressPoints} / {backlog.RequiredProgressPoints}";

            return result;
        }
    }
}
