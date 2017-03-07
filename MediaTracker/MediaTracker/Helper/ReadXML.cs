using MediaTracker.Classes;
using System;
using System.Collections.Generic;
using System.Xml;

namespace MediaTracker.Helper
{
    public class ReadXML
    {
        public List<Movie> ReadMovie(string FilePath)
        {
            XmlTextReader textReader = new XmlTextReader(FilePath);
            textReader.WhitespaceHandling = WhitespaceHandling.None;
            List<Movie> movieList = new List<Movie>();

            textReader.ReadToDescendant("MovieList");

            while (textReader.ReadToFollowing("Movie"))
            {
                Movie tempMovie = new Movie();

                textReader.ReadToDescendant("Title");
                tempMovie.Title = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("PersonalRating");
                tempMovie.PersonalRating = ushort.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("DateAdded");
                tempMovie.DateAdded = DateTime.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("Genre");
                tempMovie.Genre = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("ReleaseDate");
                tempMovie.ReleaseDate = DateTime.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("TimesUsed");
                tempMovie.TimesUsed = ushort.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("DateLastUsed");
                tempMovie.DateLastUsed = DateTime.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("MetacriticScore");
                tempMovie.MetacriticScore = ushort.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("MPAA");
                tempMovie.MPAA = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("Studio");
                tempMovie.Studio = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("IMDB");
                tempMovie.IMDB = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("Director");
                tempMovie.Director = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("Starring");
                tempMovie.Starring = textReader.GetAttribute("name");

                movieList.Add(tempMovie);
            }
            textReader.Close();

            return movieList;
        }

        public List<VideoGame> ReadVideoGame(string FilePath)
        {
            XmlTextReader textReader = new XmlTextReader(FilePath);
            textReader.WhitespaceHandling = WhitespaceHandling.None;
            List<VideoGame> videoGameList = new List<VideoGame>();

            textReader.ReadToDescendant("MovieList");

            while (textReader.ReadToFollowing("Movie"))
            {
                VideoGame tempVideoGame = new VideoGame();

                textReader.ReadToDescendant("Title");
                tempVideoGame.Title = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("PersonalRating");
                tempVideoGame.PersonalRating = ushort.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("DateAdded");
                tempVideoGame.DateAdded = DateTime.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("Genre");
                tempVideoGame.Genre = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("ReleaseDate");
                tempVideoGame.ReleaseDate = DateTime.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("TimesUsed");
                tempVideoGame.TimesUsed = ushort.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("DateLastUsed");
                tempVideoGame.DateLastUsed = DateTime.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("MetacriticScore");
                tempVideoGame.MetacriticScore = ushort.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("ESRB");
                tempVideoGame.ESRB = (ESRB)int.Parse(textReader.GetAttribute("name"));

                textReader.ReadToNextSibling("Platform");
                tempVideoGame.Platform = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("Publisher");
                tempVideoGame.Publisher = textReader.GetAttribute("name");

                textReader.ReadToNextSibling("Studio");
                tempVideoGame.Studio = textReader.GetAttribute("name");

                videoGameList.Add(tempVideoGame);
            }
            textReader.Close();

            return videoGameList;
        }

        public List<Music> ReadMusic(string FilePath)
        {
            return new List<Music>();
        }
    }
}
