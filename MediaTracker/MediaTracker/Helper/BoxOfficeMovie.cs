using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public class BoxOfficeMovie
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("imdb")]
        public string Imdb { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("studio")]
        public string Studio { get; set; }
        //[JsonProperty("release-date")]
        //public string ReleaseDate { get; set; }
        [JsonProperty("director")]
        public string Director { get; set; }
        [JsonProperty("starring")]
        public string Starring { get; set; }
        [JsonProperty("genre")]
        public string Genre { get; set; }
        [JsonProperty("mpaa")]
        public string Mpaa { get; set; }
        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Movies { public List<BoxOfficeMovie> data; }
}