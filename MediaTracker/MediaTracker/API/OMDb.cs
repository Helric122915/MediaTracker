using MediaTracker.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MediaTracker.API
{
    public class OMDb
    {
        string website = "http://www.omdbapi.com/?t=";

        public async Task<Movie> GetRequest(string Title, string Year, Plot Plot, Response Response)
        {
            string result = "Error Get Request Failed";
            HttpClient client = new HttpClient();

            FormUrlEncodedContent requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("text", "text") });

            Title = Title.Replace(" ", "+");

            string URL = String.Format("{0}{1}&y={2}&plot={3}&r={4}", website, Title, Year, Plot, Response);

            HttpResponseMessage response = await client.PostAsync(URL, requestContent);

            HttpContent responseContent = response.Content;

            using (StreamReader reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                result = await reader.ReadToEndAsync();

            Movie movie = new JavaScriptSerializer().Deserialize<Movie>(result);

            return movie;
        }
    }

    public enum Response
    {
        JSON,
        XML
    }

    public enum Plot
    {
        Short,
        Full
    }
}