
namespace ScrumGame.Shared
{
    /// <summary>
    /// A story is a gameplay element that is part of the backlog. In 
    /// order to win a team must clear their backlog of all stories.
    /// </summary>
    public class Story
    {
        /// <summary>
        /// Flavor text that describes this story.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Text that gets used when this story is modified using a change story card.
        /// </summary>
        public string AltDescription { get; set; }

        /// <summary>
        /// Creates a new story. Provide some text that can be used for the 
        /// story. If this story can be changed using a change story card,
        /// supply some alternate text to be used when the story is changed,
        /// otherwise, just leave it null.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="altDescription"></param>
        public Story(string description, string altDescription = null)
        {
            Description = description;
            AltDescription = altDescription;
        }
    }
}
