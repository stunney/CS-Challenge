using JokeGenerator.PrivServProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JokeGenerator.Tests
{
    [TestClass]
    public class PrivServNamesHttpProviderTests
    {
        [DataTestMethod]
        [DataRow(1, DisplayName = "SingleName")]
        [DataRow(50, DisplayName = "FiftyNames")]
        public void GetNamesTest(int count)
        {
            PrivServNamesHttpProvider provider = new PrivServNamesHttpProvider();
            IList<KeyValuePair<string,string>> ret = provider.GetNames(count);
            Assert.AreEqual(count, ret.Count);
            foreach(KeyValuePair<string, string> name in ret)
            {
                Assert.IsFalse(string.IsNullOrEmpty(name.Key));
                Assert.IsFalse(string.IsNullOrEmpty(name.Value));
            }
        }
    }
}
