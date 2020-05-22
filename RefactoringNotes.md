Thoughts:

#To Start
0) It compiles.  Phew!
1) No tests.
2) JsonFeed is a mixed bag of a static and instance object.  Also does way too many things at once.
3) ConsolePrinter, same thing.  ToString() violates the "unwritten" contract of ToString().  Can't be used by comparitors and does two(2) things when it should do one (return a string)
4) Program has a large amount of code in it that seems boilerplate and will be replaced by a proper library for parsing out command line arguments and we will pull out the logic into separate classes

#First Step - Create a meaningful test for JsonFeed
This class appears to do a few different things, this is an internal class so breaking backward compatability is not a concern.

[BUG]
Testing GetRandomJobFromWorkingUrl fails at first, getting an HTTP 404 error.  The code inside appends /jokes/random to the URL but this is VERY provider-specific.

[REFACTORING]
Conclusion:  Pull the ChuckNorris joke feed out as a IJokeProvider (maybe more interfaces to come)

[BUG]
GetRandomJokes seems to only replace the first instance of "Juck Norris" with the provided name values.

[EXTERNAL BUG https://github.com/chucknorris-io/chuck-api/issues/32 (yes, I logged it)]
Tried to paste the example JSON from api.chucknorris.io as a class in Visual Studio.  Website's example has invalid JSON data, missing ',' at the end of the "url" line.  Tsk Tsk.
[EXTERNAL BUG https://github.com/chucknorris-io/chuck-api/issues/33 (yes, I logged it)]
Using the "money" category along with a name replacement in the API returns an invalid 404 error.  Other categories with the same URI work as expected. Might return a 204 (no content)

[Status Update]
ChuckNorris* classes defined and unit tested.

[REFACTORING]
Moving on to the Names portion of the JsonFeed class.  Another provider being created.

privserv.com seems to provide a bulk function for getting names.  I'll look into this.
https://names.privserv.com/api/?amount=25 (min 1, max 500 according to the docs)
Other switches such as gender and region are available, max length of the name, etc.

PrivServ provider coded up for names and tests added.

JsonFeed class is now largely useless.  Time to bring in a factory pattern for providing interfaces to a controller.

[REFACTORING]
The Cambrian Explosion just occured.  A lot of new fossils to maintain but they are all very small and shared interfaces are now being used so that proper factory patterns and dependency injection can be used going forwad.

[REFACTORING]
ConsolePrinter is next :)  I just deleted the file

A simple regex search and replace made Program.cs a whole lot easier to read (CTRL-H)
Find: printer.Value(.*)\.ToString\(\);
Replace: Console.WriteLine$1;

[BUG]
In Program.cs, if the user enters 'n' (or No) when asked "Want to use a random name? y/n" then the program doesn't load any names

[BUG]
When entering a number (say, the count of jokes you want) and you type something that Int.Parse can't handle the user will see a stack trace barf all over their screen.

[BUG]
Once you enter the program and press '?' you can never quit!

[IMPROVEMENT]
Adding the ability to localize the strings on the screen for multiple regions and dialects.  Did some more refactoring as well.