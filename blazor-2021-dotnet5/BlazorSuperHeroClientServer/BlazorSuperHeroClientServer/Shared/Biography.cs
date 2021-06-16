using System.Collections.Generic;

namespace BlazorSuperHeroClientServer.Shared
{
    public class Biography
    {
        public string fullName { get; set; }
        public string alterEgos { get; set; }
        public List<string> aliases { get; set; }
        public string placeOfBirth { get; set; }
        public string firstAppearance { get; set; }
        public string publisher { get; set; }
        public string alignment { get; set; }
    }
}
