using MediaTracker.Helper;
using System;
using System.Collections.Generic;

namespace MediaTracker.Classes
{
    public class Music : Media
    {
        public string Artist { get; set; }
        public string Length { get; set; }
        public List<string> TrackList { get; set; }

        public Music()
        {
            TrackList = new List<string>();
        }

        public Music(MusicGraphAlbum album)
        {
            TrackList = new List<string>();
            Title = album.title;
            PersonalRating = 0;
            DateAdded = DateTime.Now;
            Genre = album.main_genre;
            try
            {
                ReleaseDate = (album.release_date != "" ? DateTime.Parse(album.release_date) : new DateTime());
            }
            catch
            {
                try
                {
                    ReleaseDate = (album.release_year != 0 ? new DateTime(album.release_year, 1, 1) : new DateTime());
                }
                catch { }
            }
            TimesUsed = 0;
            DateLastUsed = new DateTime();
            Artist = album.artist_name;
        }
    }
}