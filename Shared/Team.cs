using ScrumGame.Shared.Players;

namespace ScrumGame.Shared
{
    public class Team
    {
        public Backlog Backlog { get; set; } = new Backlog();
        public Player[] Players { get; set; }

        public Team(params Player[] players)
        {
            Players = players;

            foreach (var player in Players)
            {
                player.Team = this;
            }
        }
    }
}
