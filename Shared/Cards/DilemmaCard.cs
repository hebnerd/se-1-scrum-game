
namespace ScrumGame.Shared.Cards
{
    /// <summary>
    /// Dilemma cards are drawn by product owners. They should generally 
    /// have mixed effects.
    /// </summary>
    public class DilemmaCard : PlanningCard
    {
        public DilemmaCard(string description, int devPower, int progressPoints, int requiredProgressPoints) : base(description, devPower, progressPoints, requiredProgressPoints)
        {

        }
    }
}
