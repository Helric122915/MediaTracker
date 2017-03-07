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
            Genre = game.genres.ToString();
            ReleaseDate = new DateTime();
            TimesUsed = 0;
            DateLastUsed = DateTime.Now;
            MetacriticScore = 0;
            ESRB = (ESRB)game.esrb.rating;
            Platform = "";
            Publisher = game.publishers.ToString();
            Studio = game.developers.ToString();
        }
    }
}