using System.Collections.Generic;

namespace JokeGenerator
{
    public interface IJokeNameProvider
    {
        IList<KeyValuePair<string, string>> GetNames(int count);
    }
}