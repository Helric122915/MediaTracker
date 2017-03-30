using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unirest_net.http;
using MediaTracker.Helper;
using System.Web.Script.Serialization;
using System;

namespace MediaTracker.API
{
    public class IGDB
    {
        string APIKey = "Nj8rTlwu2pmshpHLtu4h5jMjXTzop16XhFcjsnOnd9XP9v61Xz";
        string website = "https://igdbcom-internet-game-database-v1.p.mashape.com/";

        public async Task<List<IGDBVideoGame>> GetVideoGameRequest(string title)
        {
            string url = website + "games/?fields=*&limit=50&offset=0&order=release_dates.date%3Adesc&search=";

            title = title.Replace(" ", "+");
            url = url + title;

            Task<HttpResponse<string>> request = Unirest.get(url)
                .header("X-Mashape-Key", APIKey)
                .header("Accept", "application/json")
                .asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
            {
                List<IGDBVideoGame> videoGame = new JavaScriptSerializer().Deserialize<List<IGDBVideoGame>>(result);

                videoGame = videoGame.OrderBy(o => o.name).ToList();

                return videoGame;
            }
            catch
            {
            }
            return new List<IGDBVideoGame>();
        }

        public async Task<string> GetCompanyRequest(string company)
        {
            string url = website + "companies/" + company + "?fields=*&limit=50&offset=0";

            Task<HttpResponse<string>> request = Unirest.get(url)
                .header("X-Mashape-Key", APIKey)
                .header("Accept", "application/json")
                .asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
            {
                List<IGDBCompany> videoGame = new JavaScriptSerializer().Deserialize<List<IGDBCompany>>(result);

                IEnumerable<string> returnString = videoGame.Select(o => string.Join("", o.name));
                string joined = string.Join(",", returnString);

                return joined;
            }
            catch
            {
            }
            return "";
        }

        public async Task<string> GetGenreRequest(string genre)
        {
            string url = website + "genres/" + genre + "?fields=*&limit=50&offset=0";

            Task<HttpResponse<string>> request = Unirest.get(url)
                .header("X-Mashape-Key", APIKey)
                .header("Accept", "application/json")
                .asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
            {
                List<IGDBGenre> videoGame = new JavaScriptSerializer().Deserialize<List<IGDBGenre>>(result);

                IEnumerable<string> returnString = videoGame.Select(o => string.Join("", o.name));
                string joined = string.Join(",", returnString);

                return joined;
            }
            catch
            {
            }
            return "";
        }

        public async Task<string> GetReleaseDateRequest(int gameID)
        {
            string url = website + "release_dates/" + gameID + "?fields=*&limit=1";

            Task<HttpResponse<string>> request = Unirest.get(url)
                .header("X-Mashape-Key", APIKey)
                .header("Accept", "application/json")
                .asJsonAsync<string>();

            HttpResponse<string> response = await request;
            string result = response.Body;

            try
            {
                List<IGDBReleaseDate> videoGame = new JavaScriptSerializer().Deserialize<List<IGDBReleaseDate>>(result);

                return "";
            }
            catch
            {
            }
            return "";
        }
    }
}