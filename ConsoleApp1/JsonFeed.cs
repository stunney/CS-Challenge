using System.Collections.Generic;
using JokeCompany;
using JokeGenerator;
using JokeGenerator.ChuckNorrisProvider;
using JokeGenerator.PrivServProvider;

namespace ConsoleApp1
{
    internal class JsonFeed
    {
		public static IList<string> GetRandomJokes(JokeRequest jokeRequest)
		{
            IJokeProvider jp = new ChuckNorrisHttpJokeProvider();
            return jp.GetJokes(jokeRequest);
        }

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static IList<KeyValuePair<string,string>> Getnames(int count)
		{
            IJokeNameProvider jnp = new PrivServNamesHttpProvider();
            return jnp.GetNames(count);
		}

		public static IList<string> GetCategories()
		{
			IJokeCategoryProvider cp = new ChuckNorrisCategoryProvider();
			return cp.GetCategories();
		}
    }
}
