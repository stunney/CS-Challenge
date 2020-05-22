using JokeCompany;
using JokeGenerator.ChuckNorrisProvider;
using JokeGenerator.PrivServProvider;

namespace JokeGenerator
{
    /// <summary>
    /// This factory will provide all of the mechanisms required to make some really amazing jokes.  Hold on!!
    /// </summary>
    /// <remarks>This is where the dynamic ability to add new providers would come into play.</remarks>
    public sealed class JokeFactory
    {
        public IJokeCategoryProvider JokeCategoryProvider { get; } = new ChuckNorrisCategoryProvider();

        public IJokeNameProvider JokeNameProvider { get; } = new PrivServNamesHttpProvider();

        public IJokeProvider JokeProvider { get; } = new ChuckNorrisHttpJokeProvider();
    }
}