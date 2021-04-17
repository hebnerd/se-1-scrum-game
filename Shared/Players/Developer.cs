
namespace ScrumGame.Shared.Players
{
    public class Developer : Player
    {
        /// <summary>
        /// Dev Power is used to determine how quickly developers can complete 
        /// stories. The higher a developer's Dev Power is, the faster a story 
        /// can be completed.
        /// </summary>
        public int DevPower { get; set; }

        public Developer(string name) : base(name)
        {
            
        }        
    }
}
