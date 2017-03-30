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
    public class BoxOfficeDB
    {
        string APIKey = "Nj8rTlwu2pmshpHLtu4h5jMjXTzop16XhFcjsnOnd9XP9v61Xz";
        string website = "https://boxofficebuz.p.mashape.com/v1/movie/";

        public async Task<List<BoxOfficeMovie>> GetRequest(string title)
        {
            title = title.Replace(" ", "%20");
            string url = website + "search/" + title;

            Task<HttpResponse<string>> request = Unirest.get(url)
                .header("X-Mashape-Key", APIKey).asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
            {
                Movies movies = new JavaScriptSerializer().Deserialize<Movies>(result);

                movies.data = movies.data.OrderBy(o => o.Title).ToList();

                return movies.data;
            }
            catch
            {
            }
            return new List<BoxOfficeMovie>();
        }

        public async Task<string> GetReleaseDateRequest(int movieID)
        {
            string url = website + movieID + "/release_dates";

            Task<HttpResponse<string>> request = Unirest.get(url)
                .header("X-Mashape-Key", APIKey)
                .header("Accept", "application/json")
                .asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
            {
                BODBReleaseDate releaseDates = new JavaScriptSerializer().Deserialize<BODBReleaseDate>(result);

                Datum USA = releaseDates.data.Find(o => o.iso_3166_1 == "US");

                return USA.release_date;
            }
            catch
            {
                return "";
            }
        }
    }
}