using System;

namespace JokeGenerator
{
    [Serializable]
    public sealed class JokeRequest
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int JokeCount { get; private set; }
        public string Category { get; private set; }

        public JokeRequest(int jokeCount)
        {
            if (0 > jokeCount) throw new ArgumentOutOfRangeException("jokeCount");
            JokeCount = jokeCount;
        }

        public JokeRequest(string category, int jokeCount)
            : this(jokeCount)
        {
            //if (string.IsNullOrEmpty(category)) throw new ArgumentNullException("category");
            Category = category;            
        }

        public JokeRequest(string firstName, string lastName, string category, int jokeCount)
            : this(category, jokeCount)
        {
            //Not sure if we should allow no names here.  Maybe someone just wants to see raw jokes.  Keep the business rules out of here for now.
            //if (string.IsNullOrEmpty(firstName)) throw new ArgumentNullException("firstName");
            //if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException("lastName");                       

            FirstName = firstName;
            LastName = lastName;
            
        }
    }
}