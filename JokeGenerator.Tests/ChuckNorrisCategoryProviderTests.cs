using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JokeGenerator.ChuckNorrisProvider;

namespace JokeGenerator.Tests
{
    [TestClass]
    public class ChuckNorrisCategoryProviderTests
    {
        [TestMethod]
        public void GetAllCategoriesTest()
        {
            IJokeCategoryProvider categoryProvider = new ChuckNorrisCategoryProvider();
            IList<string> ret = categoryProvider.GetCategories();

            Assert.IsNotNull(ret);
            Assert.AreNotEqual(0, ret.Count);
        }
    }
}