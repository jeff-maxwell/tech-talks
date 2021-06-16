using System.Collections.Generic;

namespace BlazorSuperHeroClientServer.Shared
{
    public class Appearance
    {
        public string gender { get; set; }
        public string race { get; set; }
        public List<string> height { get; set; }
        public List<string> weight { get; set; }
        public string eyeColor { get; set; }
        public string hairColor { get; set; }
    }
}
