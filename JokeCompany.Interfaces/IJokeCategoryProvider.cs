using System.Collections.Generic;

namespace JokeCompany
{
    public interface IJokeCategoryProvider
    {
        IList<string> GetCategories();
    }
}
