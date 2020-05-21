using System.Collections.Generic;

namespace JokeGenerator
{
    public interface IJokeCategoryProvider
    {
        IList<string> GetCategories();
    }
}
