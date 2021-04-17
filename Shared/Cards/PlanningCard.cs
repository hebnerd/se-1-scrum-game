using ScrumGame.Shared.Players;

namespace ScrumGame.Shared.Cards
{
    /// <summary>
    /// A planning card is a type of card that can increase or decrease 
    /// dev power, progress points, or required progress points when 
    /// played. Planning cards are abstract and need further implementation.
    /// </summary>
    public abstract class PlanningCard : Card
    {
        /// <summary>
        /// Dev Power this card will apply when played.
        /// </summary>
        public int DevPower { get; set; }
        /// <summary>
        /// Progress points this card will add or subtract when played.
        /// </summary>
        public int ProgressPoints { get; set; }
        /// <summary>
        /// Adds or subtracts to the number of progress points currently
        /// required on the backlog.
        /// </summary>
        public int RequiredProgressPoints { get; set; }

        /// <summary>
        /// Planning cards change dev power, progress points, and required progress points.
        /// </summary>
        /// <param name="description">The descriptive text of the card.</param>
        /// <param name="devPower">The change to all of the developers' power.</param>
        /// <param name="progressPoints">The change in progress points.</param>
        /// <param name="requiredProgressPoints">The required progress points.</param>
        protected PlanningCard(string description, int devPower, int progressPoints, int requiredProgressPoints) : base(description)
        {
            DevPower = devPower;
            ProgressPoints = progressPoints;
            RequiredProgressPoints = requiredProgressPoints;
        }

        /// <summary>
        /// When a planning card is played, it can change the dev power of all 
        /// the devs, the progress points, or the required progress points.
        /// </summary>
        /// <param name="player">The player who played this card.</param>
        public override void Play(Player player)
        {
            if (ProgressPoints != 0)
                player.Team.Backlog.CurrentProgressPoints += ProgressPoints;

            if (RequiredProgressPoints != 0)
                player.Team.Backlog.RequiredProgressPoints += RequiredProgressPoints;

            if (DevPower != 0)
            {
                foreach (var ply in player.Team.Players)
                {
                    if (ply is Developer dev)
                    {
                        dev.DevPower += DevPower;
                    }
                }
            }
        }
    }
}
