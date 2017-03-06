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
    public class IGDB
    {
        string APIKey = "Nj8rTlwu2pmshpHLtu4h5jMjXTzop16XhFcjsnOnd9XP9v61Xz";
        string website = "https://igdbcom-internet-game-database-v1.p.mashape.com/games/?fields=*&limit=50&offset=0&order=release_dates.date%3Adesc&search=";

        public async Task<List<IGDBVideoGame>> GetRequest(string title)
        {
            title = title.Replace(" ", "+");
            string url = website + title;

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
    }
}