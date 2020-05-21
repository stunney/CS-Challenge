using JokeGenerator.ChuckNorrisProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JokeGenerator.Tests
{
    [TestClass]
    public class ChuckNorrisHttpJokeProviderTests
    {
        //Valid Categories from https://api.chucknorris.io/jokes/categories are
        //["animal","career","celebrity","dev","explicit","fashion","food","history","money","movie","music","political","religion","science","sport","travel"]
        //Notes:  "money" returns a 404.  What is THAT all about?!

        [DataTestMethod]
        [DataRow(1, DisplayName = "SingleJoke")]
        [DataRow(3, DisplayName = "ThreeJokes")]
        [DataRow(50, DisplayName ="FiftyJokes")]
        public void GetRenamedFirstNameOnlyJokesTest(int count)
        {
            var jokeProvider = new ChuckNorrisHttpJokeProvider();
            JokeRequest jr = new JokeRequest("Bob", null, "history", count);
            IList<string> results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }

        [DataTestMethod]
        [DataRow(1, DisplayName = "SingleJoke")]
        [DataRow(3, DisplayName = "ThreeJokes")]
        [DataRow(50, DisplayName = "FiftyJokes")]
        public void GetRenamedLastNameOnlyJokesTest(int count)
        {
            var jokeProvider = new ChuckNorrisHttpJokeProvider();
            JokeRequest jr = new JokeRequest(null, "Smith", "food", count);
            IList<string> results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }

        [DataTestMethod]
        [DataRow(1, DisplayName = "SingleJoke")]
        [DataRow(3, DisplayName = "ThreeJokes")]
        [DataRow(50, DisplayName = "FiftyJokes")]
        public void GetRenamedBothNamesJokesTest(int count)
        {
            var jokeProvider = new ChuckNorrisHttpJokeProvider();
            JokeRequest jr = new JokeRequest("Bob", "Smith", "food", count);
            IList<string> results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }

        [DataTestMethod]
        [DataRow(1, DisplayName = "SingleJoke")]
        [DataRow(3, DisplayName = "ThreeJokes")]
        [DataRow(50, DisplayName = "FiftyJokes")]
        public void GetNoNameJustCategoryJokesTest(int count)
        {
            var jokeProvider = new ChuckNorrisHttpJokeProvider();
            JokeRequest jr = new JokeRequest("food", count);
            IList<string> results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }


        [DataTestMethod]
        [DataRow(1, DisplayName = "SingleJoke")]
        [DataRow(3, DisplayName = "ThreeJokes")]
        [DataRow(50, DisplayName = "FiftyJokes")]
        public void GetAnyJokesTest(int count)
        {
            var jokeProvider = new ChuckNorrisHttpJokeProvider();
            JokeRequest jr = new JokeRequest(count);
            IList<string> results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }
    }
}