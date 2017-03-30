using MediaTracker.Helper;
using System;

namespace MediaTracker.Classes
{
    public class Movie : Media
    {
        public MPAA MPAA { get; set; }
        public string Studio { get; set; }
        public string IMDB { get; set; }
        public string Director { get; set; }
        public string Starring { get; set; }

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
            MPAA temp = MPAA.None;
            Enum.TryParse<MPAA>(movie.Mpaa, out temp);
            MPAA = temp;
            Studio = movie.Studio;
            IMDB = movie.Imdb;
            Director = movie.Director;
            Starring = movie.Starring;
        }
    }
}