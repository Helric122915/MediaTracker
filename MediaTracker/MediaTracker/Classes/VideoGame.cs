using MediaTracker.Helper;
using System;

namespace MediaTracker.Classes
{
    public class VideoGame : Media
    {
        public ESRB ESRB { get; set; }
        public string Publisher { get; set; }
        public string Studio { get; set; }

        public VideoGame() { }

        public VideoGame(IGDBVideoGame game)
        {
            Title = game.name;
            PersonalRating = 0;
            DateAdded = DateTime.Now;
            Genre = (game.genres != null ? string.Join(",", game.genres.ToArray()) : "");
            ReleaseDate = new DateTime();
            TimesUsed = 0;
            DateLastUsed = DateTime.Now;
            ESRB = (game.esrb != null ? (ESRB)game.esrb.rating : ESRB.None);
            Publisher = (game.publishers != null ? string.Join(",", game.publishers.ToArray()) : "");
            Studio = (game.developers != null ? string.Join(",", game.developers.ToArray()) : "");
        }
    }
}