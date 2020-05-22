namespace JokeCompany.Interfaces
{
    public sealed class JokeRequestBuilder
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }
        public int JokeCount { get; set; }

        public JokeRequest ToJokeRequest()
        {
            if(null == Category)
            {
                return new JokeRequest(JokeCount);
            }

            if(null == FirstName && null == LastName)
            {
                return new JokeRequest(Category, JokeCount);
            }

            return new JokeRequest(FirstName, LastName, Category, JokeCount);            
        }
    }
}