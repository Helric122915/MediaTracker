using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public class Writer
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Datum_Track
    {
        public double popularity { get; set; }
        public string track_artist_id { get; set; }
        public string track_index { get; set; }
        public int duration { get; set; }
        public string main_genre { get; set; }
        public string track_spotify_id { get; set; }
        public string entity_type { get; set; }
        public List<Writer> writer { get; set; }
        public string track_ref_id { get; set; }
        public string id { get; set; }
        public int original_release_year { get; set; }
        public string track_youtube_id { get; set; }
        public string track_musicbrainz_id { get; set; }
        public string artist_name { get; set; }
        public string isrc { get; set; }
        public int release_year { get; set; }
        public string track_album_id { get; set; }
        public string title { get; set; }
        public string album_title { get; set; }
    }

    public class MusicGraphTracks
    {
        public Status status { get; set; }
        public Pagination pagination { get; set; }
        public List<Datum_Track> data { get; set; }
    }
}
