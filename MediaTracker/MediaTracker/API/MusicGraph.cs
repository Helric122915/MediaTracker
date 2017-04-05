using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using unirest_net.http;
using MediaTracker.Helper;
using System.Web.Script.Serialization;

namespace MediaTracker.API
{
    public class MusicGraph
    {
        string APIKey = "d9b1825bab5be513e3e500a80b3f3d15";
        string website = "http://api.musicgraph.com/api/v2/";

        public async Task<List<MusicGraphAlbum>> GetAlbumRequest(string title)
        {
            title = title.Replace(" ", "%20");
            string url = website + "album/suggest?api_key=" + APIKey + "&prefix=" + title + "&limit=50";

            Task<HttpResponse<string>> request = Unirest.get(url).asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
           {
                MusicGraphMusic albums = new JavaScriptSerializer().Deserialize<MusicGraphMusic>(result);

                albums.data = albums.data.OrderBy(o => o.title).ToList();

                return albums.data;
            }
            catch
            {
            }
            return new List<MusicGraphAlbum>();
        }

        public async Task<List<MusicGraphAlbum>> GetAlbumByArtistRequest(string title)
        {
            title = title.Replace(" ", "%20");
            string url = website + "album/search?api_key=" + APIKey + "&artist_name=" + title + "&limit=50";

            Task<HttpResponse<string>> request = Unirest.get(url).asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
            {
                MusicGraphMusic albums = new JavaScriptSerializer().Deserialize<MusicGraphMusic>(result);

                albums.data = albums.data.OrderBy(o => o.title).ToList();

                return albums.data;
            }
            catch
            {
            }
            return new List<MusicGraphAlbum>();
        }

        public async Task<List<Datum_Track>> GetAlbumMetaData(string musicID)
        {
            string url = website + "album/" + musicID + "/tracks" + "?api_key=" + APIKey;

            Task<HttpResponse<string>> request = Unirest.get(url).asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
            {
                MusicGraphTracks tracks = new JavaScriptSerializer().Deserialize<MusicGraphTracks>(result);

                return tracks.data;
            }
            catch
            {
                return new List<Datum_Track>();
            }
        }
    }
}