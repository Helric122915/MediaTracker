using System.Collections.Generic;

namespace MediaTracker.Helper
{
    public class Status
    {
        public int code { get; set; }
        public string message { get; set; }
        public string api { get; set; }
    }

    public class Pagination
    {
        public int count { get; set; }
        public int total { get; set; }
        public int offset { get; set; }
    }

    public class MusicGraphAlbum
    {
        public string number_of_tracks { get; set; }
        public string artist_name { get; set; }
        public int release_year { get; set; }
        public string entity_type { get; set; }
        public string release_date { get; set; }
        public double popularity { get; set; }
        public string album_artist_id { get; set; }
        public string main_genre { get; set; }
        public string number_of_releases { get; set; }
        public string album_musicbrainz_id { get; set; }
        public string album_ref_id { get; set; }
        public string product_form { get; set; }
        public string title { get; set; }
        public string id { get; set; }
        public object decade { get; set; }
    }

    public class MusicGraphMusic
    {
        public Status status { get; set; }
        public Pagination pagination { get; set; }
        public List<MusicGraphAlbum> data { get; set; }
    }
}
