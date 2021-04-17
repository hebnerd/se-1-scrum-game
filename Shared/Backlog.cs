using System.Collections.Generic;

namespace ScrumGame.Shared
{
    public class Backlog
    {
        public int CurrentProgressPoints { get; set; }
        public int RequiredProgressPoints { get; set; } = 4;
        public readonly List<Story> Stories = new List<Story>();
    }
}
