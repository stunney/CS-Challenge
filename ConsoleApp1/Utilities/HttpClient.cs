using System;

namespace JokeGenerator.Utilities
{
    public class HttpClient : System.Net.Http.HttpClient
    {        
        private UriBuilder m_uriBuilder = new UriBuilder();

        public HttpClient() : base() { }

        public virtual void AppendToQuery(string key, string value)
        {
            string queryToAppend = $"{key}={value}";
            if (m_uriBuilder.Query != null && m_uriBuilder.Query.Length > 1)
                m_uriBuilder.Query = m_uriBuilder.Query.Substring(1) + "&" + queryToAppend;
            else
                m_uriBuilder.Query = queryToAppend;
        }

        public virtual string QueryString { get { return m_uriBuilder.Query; } }
    }
}
