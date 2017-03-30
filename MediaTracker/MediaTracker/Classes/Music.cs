using MediaTracker.Helper;

namespace MediaTracker.Classes
{
    public class Music : Media
    {
        public string RIAA { get; set; }
        public string Artist { get; set; }
        public string Label { get; set; }
        public string Length { get; set; } // possibly time span?
    }
}