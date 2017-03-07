using MediaTracker.Helper;
using System;

namespace MediaTracker.Classes
{
    public class Movie : Media
    {
        public string MPAA;
        public string Studio;
        public string IMDB;
        public string Director;
        public string Starring;

        public Movie() { }

        public Movie(BoxOfficeMovie movie)
        {
            Title = movie.Title;
            PersonalRating = 0;
            DateAdded = DateTime.Now;
            Genre = movie.Genre;
            ReleaseDate = new DateTime();
            TimesUsed = 0;
            DateLastUsed = DateTime.Now;
            MetacriticScore = 0;
            MPAA = movie.Mpaa;
            Studio = movie.Studio;
            IMDB = movie.Imdb;
            Director = movie.Director;
            Starring = movie.Starring;
        }
    }
}