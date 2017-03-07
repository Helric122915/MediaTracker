using MediaTracker.Helper;
using System;

namespace MediaTracker.Classes
{
    public class VideoGame : Media
    {
        public ESRB ESRB;
        public string Platform;
        public string Publisher;
        public string Studio;

        public VideoGame() { }

        public VideoGame(IGDBVideoGame game)
        {
            Title = game.name;
            PersonalRating = 0;
            DateAdded = DateTime.Now;
            Genre = string.Join(",", game.genres.ToArray());
            ReleaseDate = new DateTime();
            TimesUsed = 0;
            DateLastUsed = DateTime.Now;
            MetacriticScore = 0;
            ESRB = (game.esrb != null ? (ESRB)game.esrb.rating : ESRB.None);
            Platform = "";
            Publisher = string.Join(",", game.publishers.ToArray());
            Studio = string.Join(",", game.developers.ToArray());
        }
    }
}