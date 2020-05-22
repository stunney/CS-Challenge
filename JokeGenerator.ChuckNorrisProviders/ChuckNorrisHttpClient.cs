using System;

namespace JokeGenerator.ChuckNorrisProvider
{
    public class ChuckNorrisHttpClient : JokeCompany.Utilities.Http.HttpClient
    {
        public ChuckNorrisHttpClient(Uri absoluteBaseURI) : base()
        {
            if (null == absoluteBaseURI) throw new ArgumentNullException("absoluteBaseURI");

            base.BaseAddress = absoluteBaseURI;
            base.DefaultRequestHeaders.Add("accept", "application/json");
        }
    }
}