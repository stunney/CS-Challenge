using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using JokeGenerator.Utilities;

namespace JokeGenerator.PrivServProvider
{
    public class PrivServNamesHttpProvider : IJokeNameProvider
    {
        private const string URL = "https://names.privserv.com/api/";

        private string RequestNamesFromProvider(int count)
        {
            HttpClient c = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };
            c.DefaultRequestHeaders.Add("accept", "application/json");

            c.AppendToQuery("amount", count.ToString());
            string response = c.GetStringAsync(c.QueryString).Result;
            if (1 == count)
            {
                //Fun with deserializing JSON!
                response = $"[{response}]"; //Make it an array of 1 :)
            }
            return response;
        }

        /// <summary>
        /// Sometimes you are just going to have some data marshallers in your code.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private IList<KeyValuePair<string, string>> ParseJson(string json, int count)
        {
            IList<PrivServStandardName> names = JsonConvert.DeserializeObject<List<PrivServStandardName>>(json);

            IList<KeyValuePair<string, string>> ret = new List<KeyValuePair<string, string>>(count); //Set that capacity like a boss
            foreach (PrivServStandardName name in names)
            {
                ret.Add(new KeyValuePair<string, string>(name.name, name.surname));
            }
            return ret;
        }

        public virtual IList<KeyValuePair<string, string>> GetNames(int count)
        {
            if (0 > count)
            {
                throw new ArgumentOutOfRangeException("This provider cannot produce a negative number of names");
            }
            if (500 < count)
            {
                throw new ArgumentOutOfRangeException("This provider cannot produce more than five hundred (500) names in a single call.");
            }

            return ParseJson(RequestNamesFromProvider(count), count);
        }

        /// <summary>
        /// Codegen from Visual Studio, Paste Special JSON as Classes feature :)
        /// </summary>
        private class PrivServStandardName
        {
            public string name { get; set; }
            public string surname { get; set; }
            public string gender { get; set; }
            public string region { get; set; }
        }
    }
}