using System.Collections.Generic;

namespace JokeCompany
{
    public interface IJokeNameProvider
    {
        IList<KeyValuePair<string, string>> GetNames(int count);
    }
}