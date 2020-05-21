using System.Collections.Generic;

namespace JokeGenerator
{
    public interface IJokeProvider
    {
        IList<string> GetJokes(JokeRequest jokeRequest);
    }
}