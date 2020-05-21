using Microsoft.VisualStudio.TestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JokeGeneratorTests
{
    [TestClass]
    public class TestJsonFeed
    {

        [TestMethod]
        public void TetGetRandomJokeFromWorkingURL()
        {
            string url = "https://api.chucknorris.io/";
            int expectedNumberOfJokes = 1;

            string expectedFirstName = "Bob";
            string expectedLastName = "Smith";
            string expectedCategory = "test";

            new ConsoleApp1.JsonFeed(url, expectedNumberOfJokes);
            string[] results = ConsoleApp1.JsonFeed.GetRandomJokes(expectedFirstName, expectedLastName, expectedCategory);

            Assert.AreEqual(expectedNumberOfJokes, results.Length);
        }
    }
}
