using JokeCompany.Utilities.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JokeCompany.Utilities.Tests
{
    [TestClass]
    public class HttpClientTests
    {
        [TestMethod]
        public void TestQueryStringBuilder()
        {
            HttpClient c = new HttpClient();
            c.AppendToQuery("name", "joe");
        }
    }
}
