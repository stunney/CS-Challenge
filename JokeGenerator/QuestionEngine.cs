using JokeCompany;
using JokeCompany.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JokeGenerator
{
    internal class QuestionEngine
    {
        private readonly LocalizedConsoleWriter m_consoleWriter;
        private IList<string> m_categories;
        private readonly JokeFactory m_jokeFactory;

        internal QuestionEngine(LocalizedConsoleWriter consoleWriter, JokeFactory jokeFactory)
        {
            m_consoleWriter = consoleWriter;
            m_jokeFactory = jokeFactory;
        }

        private char AskQuestionsForASingleKeyStroke(string[] lines, char[] acceptableChars)
        {
            foreach (string l in lines)
            {
                m_consoleWriter.WriteLine(l);
            }

            int maxTries = 5;
            while (maxTries > 0)
            {
                char c = Console.ReadKey().KeyChar;
                foreach (char x in acceptableChars)
                {
                    if (c == x)
                    {
                        Console.WriteLine();
                        return c;
                    }
                }
                m_consoleWriter.WriteLine("AnswerOptionsAre", string.Join(",", acceptableChars));
                m_consoleWriter.WriteLine("PleaseTryAgain");
                maxTries--;
            }
            throw new ApplicationException(m_consoleWriter.GetLocalizedString("Error_IncorrectAnser"));
        }

        private int AskQuestionsForANumber(string[] lines)
        {
            foreach (string l in lines)
            {
                m_consoleWriter.WriteLine(l);
            }

            int maxTries = 5;
            while (maxTries > 0)
            {
                char c = Console.ReadKey(false).KeyChar;

                int i = 0;
                if (int.TryParse(c.ToString(), out i))
                {
                    Console.WriteLine();
                    return i;
                }

                m_consoleWriter.WriteLine("Error_IncorrectNumberValue");
                m_consoleWriter.WriteLine("PleaseTryAgain");
                maxTries--;
            }
            throw new ApplicationException(m_consoleWriter.GetLocalizedString("Error_IncorrectAnser"));
        }

        private string AskForAName(string[] lines)
        {
            foreach (string l in lines)
            {
                m_consoleWriter.WriteLine(l);
            }
            return Console.ReadLine();
        }

        private string AskForACategory(string[] lines)
        {
            foreach (string l in lines)
            {
                m_consoleWriter.WriteLine(l);
            }

            int maxTries = 5;
            while (maxTries > 0)
            {
                string requestedCategory = Console.ReadLine();

                if (m_categories.Contains(requestedCategory)) return requestedCategory;

                m_consoleWriter.WriteLine("Error_EnterValidCategory");
                Console.WriteLine(string.Join(",", m_categories));
                m_consoleWriter.WriteLine("PleaseTryAgain");
                maxTries--;
            }
            throw new ApplicationException(m_consoleWriter.GetLocalizedString("Error_IncorrectAnser"));
        }

        internal int Run()
        {
            m_consoleWriter.WriteLine("Prompt_PleaseWaitLoading");
            loadCategories();

            try
            {
                char key = AskQuestionsForASingleKeyStroke(new[] { "Prompt_IntroLine?OrQuit" }, new[] { '?' });
                if (key == '?')
                {
                    while (key != 'q')
                    {
                        JokeRequestBuilder jokeRequestBuilder = new JokeRequestBuilder();

                        key = AskQuestionsForASingleKeyStroke(new[] { "Prompt_CIsForCategories", "Prompt_RIsForRandomJokes", "Prompt_QIsToQuit" }, new[] { 'c', 'r', 'q' });

                        switch (key)
                        {
                            case 'c':
                                displayCategories();
                                break;
                            case 'r':
                                key = AskQuestionsForASingleKeyStroke(new[] { "Question_RandomName" }, new[] { 'y', 'n' });
                                switch (key)
                                {
                                    case 'y':
                                        GetNames();
                                        break;
                                    case 'n':
                                        jokeRequestBuilder.FirstName = AskForAName(new[] { "Question_FirstName", "PRESS_ENTER" });
                                        jokeRequestBuilder.LastName = AskForAName(new[] { "Question_LastName", "PRESS_ENTER" });
                                        break;
                                }

                                key = AskQuestionsForASingleKeyStroke(new[] { "Question_WantACategory" }, new[] { 'y', 'n' });
                                if (key == 'y')
                                {
                                    jokeRequestBuilder.Category = AskForACategory(new[] { "Question_EnterACategory", "PRESS_ENTER" });
                                }

                                jokeRequestBuilder.JokeCount = AskQuestionsForANumber(new[] { "Question_HowManyJokes" });

                                DisplayJokes(GetRandomJokes(jokeRequestBuilder.ToJokeRequest()));
                                break;
                            case 'q':
                                break;
                        }

                    }
                }
            }
            catch (ApplicationException appex)
            {
                m_consoleWriter.WriteLine("ExpectedExceptionMessage");
                m_consoleWriter.WriteLine(appex.Message);
                return 1;
            }
            catch (Exception ex)
            {
                m_consoleWriter.WriteLine("GenericExceptionMessage");

                Exception e = ex;
                while (e != null)
                {
                    m_consoleWriter.WriteLine(e.Message);
                    m_consoleWriter.WriteLine(e.StackTrace);
                    e = e.InnerException;
                }
                return 2;
            }
            return 0;
        }

        internal void loadCategories()
        {
            if (null == m_categories)
            {
                m_categories = m_jokeFactory.JokeCategoryProvider.GetCategories();
            }
        }

        private void displayCategories()
        {
            m_consoleWriter.WriteLine($"[{string.Join(",", m_categories)}]");
        }

        internal IList<KeyValuePair<string, string>> GetNames()
        {
            return m_jokeFactory.JokeNameProvider.GetNames(1);
        }

        internal IList<string> GetRandomJokes(JokeRequest jokeRequest)
        {
            return m_jokeFactory.JokeProvider.GetJokes(jokeRequest);
        }
        
        private void DisplayJokes(IList<string> jokes)
        {
            m_consoleWriter.WriteLine($"[{string.Join(",", jokes)}]");
        }
    }
}
