using MediaTracker.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MediaTracker.Helper
{
    public class WriteXML
    {
        private Encoding UTF8 = new UTF8Encoding();
        private XmlTextWriter textWriter;

        public void WriteMovie(List<Movie> movies, string FilePath)
        {
            textWriter = new XmlTextWriter(FilePath, UTF8);

            textWriter.WriteStartDocument();

            textWriter.Formatting = Formatting.Indented;
            textWriter.Indentation = 4;
            textWriter.QuoteChar = '\'';

            textWriter.WriteComment("MediaTracker");
            textWriter.WriteComment("Generated: " + DateTime.Now.ToString());

            textWriter.WriteStartElement("MovieList");

            foreach (Movie movie in movies)
            {
                textWriter.WriteStartElement("Movie");

                textWriter.WriteStartElement("Title");
                textWriter.WriteAttributeString("name", movie.Title);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("PersonalRating");
                textWriter.WriteAttributeString("name", movie.PersonalRating.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("DateAdded");
                textWriter.WriteAttributeString("name", movie.DateAdded.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Genre");
                textWriter.WriteAttributeString("name", movie.Genre);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("ReleaseDate");
                textWriter.WriteAttributeString("name", movie.ReleaseDate.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("TimesUsed");
                textWriter.WriteAttributeString("name", movie.TimesUsed.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("DateLastUsed");
                textWriter.WriteAttributeString("name", movie.DateLastUsed.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("MPAA");
                textWriter.WriteAttributeString("name", movie.MPAA.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Studio");
                textWriter.WriteAttributeString("name", movie.Studio);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("IMDB");
                textWriter.WriteAttributeString("name", movie.IMDB);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Director");
                textWriter.WriteAttributeString("name", movie.Director);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Starring");
                textWriter.WriteAttributeString("name", movie.Starring);
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
            }

            textWriter.WriteEndElement();

            textWriter.WriteEndDocument();

            textWriter.Close();
        }

        public void WriteVideoGame(List<VideoGame> videoGames, string FilePath)
        {
            textWriter = new XmlTextWriter(FilePath, UTF8);

            textWriter.WriteStartDocument();

            textWriter.Formatting = Formatting.Indented;
            textWriter.Indentation = 4;
            textWriter.QuoteChar = '\'';

            textWriter.WriteComment("MediaTracker");
            textWriter.WriteComment("Generated: " + DateTime.Now.ToString());

            textWriter.WriteStartElement("VideoGameList");

            foreach (VideoGame game in videoGames)
            {
                textWriter.WriteStartElement("Movie");

                textWriter.WriteStartElement("Title");
                textWriter.WriteAttributeString("name", game.Title);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("PersonalRating");
                textWriter.WriteAttributeString("name", game.PersonalRating.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("DateAdded");
                textWriter.WriteAttributeString("name", game.DateAdded.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Genre");
                textWriter.WriteAttributeString("name", game.Genre);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("ReleaseDate");
                textWriter.WriteAttributeString("name", game.ReleaseDate.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("TimesUsed");
                textWriter.WriteAttributeString("name", game.TimesUsed.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("DateLastUsed");
                textWriter.WriteAttributeString("name", game.DateLastUsed.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("ESRB");
                textWriter.WriteAttributeString("name", ((int)game.ESRB).ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Publisher");
                textWriter.WriteAttributeString("name", game.Publisher);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Studio");
                textWriter.WriteAttributeString("name", game.Studio);
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
            }

            textWriter.WriteEndElement();

            textWriter.WriteEndDocument();

            textWriter.Close();
        }

        public void WriteMusic(List<Music> musicCollection, string FilePath)
        {
            foreach (Music music in musicCollection)
            {

            }
        }
    }
}
