using ScrumGame.Shared.Cards;
using ScrumGame.Shared.Players;

namespace ScrumGame.Shared
{
    public class GameManager
    {
        public Team Team { get; set; }

        public Deck<DilemmaCard> DilemmaDeck = new Deck<DilemmaCard>
        (
            new DilemmaCard("", 0, 0, 0),
            new DilemmaCard("Your developers want to refactor some old code, but it's going to throw off your burndown chart. (+2 Dev Power, -2 Progress)", 2, -2, 0),
            new DilemmaCard("The stakeholders seem confused on exactly what they want in a certain feature. You'll want to do some feedback testing, but might need to redesign some stories. (-1 Progress, -2 Required Progress Points)", 0, -1, -2),
            new DilemmaCard("The backlog needs to be groomed, but the developers have already got started on some large stories. (-1 Dev Power, -2 Required Progress Points)", -1, 0, -2),
            new DilemmaCard("After getting some feedback, you realize the backlog is missing a few things. The devs are excited to work on the new tasks, however. (+1 Dev Power, -1 Progress, -1 Required Progress Points)", 1, -1, -1)
        );

        public Deck<EventCard> EventCardDeck = new Deck<EventCard>
        (
            new EventCard("", p => { }),
            new EventCard("The developers just finished a story in a quick sprint. (Finish the first story instantly.)", p =>
            {
                p.Team.Backlog.Stories.RemoveAt(0);
                p.Team.Backlog.CurrentProgressPoints = 0;
            }),

            new EventCard("Management approved the purchase of your new cloud server which helps your developers test the database prototype. (Doubles every Developer's Dev Power.)", p =>
            {
                foreach (var player in p.Team.Players)
                {
                    if (player is Developer d)
                    {
                        d.DevPower *= 2;
                    }
                }
            }),

            new EventCard("The development team has made progress in fixing the bugs introduced in the last commit. (+2 Dev Power to the card player, and doubles progress points.)", p =>
            {
                if (p is Developer d)
                {
                    d.DevPower += 2;
                }

                p.Team.Backlog.CurrentProgressPoints *= 2;
            }),

            new EventCard("You successfully clarify some of the Owner's requirements to the developers. (All developers gain +1 Dev Power, and halve the required progress points.)", p =>
            {
                foreach (var player in p.Team.Players)
                {
                    if (player is Developer d)
                    {
                        d.DevPower += 1;
                    }
                }

                p.Team.Backlog.RequiredProgressPoints /= 2;
            })
        );

        public Deck<ImplementationCard> ImplementationDeck = new Deck<ImplementationCard>
        (
            new ImplementationCard("", 0),
            new ImplementationCard("Complete unit tests. (+1 Progress)", 1),
            new ImplementationCard("Finish up the grid layout on the main view. (+1 Progress)", 1),
            new ImplementationCard("Get the date formatted correctly. (+2 Progress)", 2),
            new ImplementationCard("Finish up the last module. (+2 Progress)", 2),
            new ImplementationCard("Find a good library to save some time. (+3 Progress)", 3),
            new ImplementationCard("Just find the code on Stack Overflow. (+3 Progress)", 3)
        );

        public Deck<StoryChangeCard> StoryChangeDeck = new Deck<StoryChangeCard>
        (
            new StoryChangeCard("", 0, 0, 0),
            new StoryChangeCard("Reasses story requirements, helps the devs get a better understanding, but wastes some time. (+1 Dev Power, -1 Progress)", 1, -1, 0),
            new StoryChangeCard("Rush the developers. (-1 Dev Power, +1 Progress)", -1, 1, 0),
            new StoryChangeCard("Gain feedback from the client, helping refine the requirments of the story. (-1 Required Progress Points)", 0, 0, -1),
            new StoryChangeCard("Give the developer mandetory training. Wastes conisderable time, but improves dev power.", 3, -2, 0),
            new StoryChangeCard("Tell the devlopers to scrap a module. They get their work done faster, but don't like it. (-1 Dev Power, -1 Requires Progress Points)", -1, 0, -1)
        );
        
        public GameManager(params Player[] players)
        {
            Team = new Team(players);
            Team.Backlog.Stories.Add(new Story("The home screen should display food offered in six categories, with two sub categories.", "The home screen should display food offered in four categories"));
            Team.Backlog.Stories.Add(new Story("Clicking a category on the home screen shows all the food offered."));
            Team.Backlog.Stories.Add(new Story("Clicking item details on a food shows nutrition info, size, price, and amount.", "Clicking item details on a food shows size, price, and amount."));
            Team.Backlog.Stories.Add(new Story("Items can be deleted from the cart. Multiple items can be selected and removed all at once.", "Items can be deleted from the cart."));            
        }
    }
}
