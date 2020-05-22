using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using JokeCompany;

namespace JokeGenerator.ChuckNorrisProvider
{
    public class ChuckNorrisCategoryProvider : IJokeCategoryProvider
    {
        private const string URL = "https://api.chucknorris.io"; //TODO:  Read in from Settings.settings

        public ChuckNorrisCategoryProvider()
        { }

        public IList<string> GetCategories()
        {
            var client = new ChuckNorrisHttpClient(new Uri(URL));
            var response = client.GetStringAsync(new Uri($"/jokes/categories", UriKind.Relative)).Result;
            JArray categories = JsonConvert.DeserializeObject<JArray>(response);

            List<string> retval = new List<string>(categories.Count);
            foreach(JToken cat in categories)
            {
                retval.Add(cat.ToString());
            }            
            return retval;
        }
    }
}
