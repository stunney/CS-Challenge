using System.Collections.Generic;

namespace JokeCompany
{
    public interface IJokeProvider
    {
        IList<string> GetJokes(JokeRequest jokeRequest);
    }
}