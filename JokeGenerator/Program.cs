using JokeGenerator;

namespace ConsoleApp1
{
    class Program
    {
        static int Main(string[] args)
        {
            LocalizedConsoleWriter consoleWriter = new LocalizedConsoleWriter();
            JokeFactory jokeFactory = new JokeFactory();

            QuestionEngine qed = new QuestionEngine(consoleWriter, jokeFactory);

            return qed.Run();           
        }
    }
}