using System;
using System.Collections.Generic;
using JokeGenerator.Utilities;
using Newtonsoft.Json;

namespace JokeGenerator.ChuckNorrisProvider
{
    public class ChuckNorrisHttpJokeProvider : IJokeProvider
    {
        private const string URL = "https://api.chucknorris.io"; //TODO:  Read in from Settings.settings

        public ChuckNorrisHttpJokeProvider()            
        {            
        }

        private string BuildQueryString(JokeRequest jokeRequest, HttpClient client)
        {
            if(string.IsNullOrEmpty(jokeRequest.FirstName)) //We can use LastName only
            {
                client.AppendToQuery("name", jokeRequest.LastName);
            }
            else if(string.IsNullOrEmpty(jokeRequest.LastName)) //We can use FirstName only
            {
                client.AppendToQuery("name", jokeRequest.FirstName);
            }
            else if(!string.IsNullOrEmpty(jokeRequest.FirstName) && !string.IsNullOrEmpty(jokeRequest.LastName)) //Use Both!
            {
                client.AppendToQuery("name", $"{jokeRequest.FirstName} {jokeRequest.LastName}");
            }

            if (!string.IsNullOrEmpty(jokeRequest.Category))
            {
                client.AppendToQuery("category", jokeRequest.Category);
            }

            return client.QueryString;
        }

        public virtual IList<string> GetJokes(JokeRequest jokeRequest)
        {
            var client = new ChuckNorrisHttpClient(new Uri(URL));
            string qs = BuildQueryString(jokeRequest, client);

            List<string> jokes = new List<string>(jokeRequest.JokeCount); //Set that capacity like a boss

            for (int i = 0; i < jokeRequest.JokeCount; i++)
            {
                var response = client.GetStringAsync(new Uri($"/jokes/random{qs}", UriKind.Relative)).Result;
                
                var joke = JsonConvert.DeserializeObject<ChuckNorrisHttpResponseJoke>(response).value;
                jokes.Add(joke);
            }

            return jokes;
        }


        private class ChuckNorrisHttpResponseJoke
        {
            public string icon_url { get; set; }
            public string id { get; set; }
            public string url { get; set; }
            public string value { get; set; }
        }
    }
}
