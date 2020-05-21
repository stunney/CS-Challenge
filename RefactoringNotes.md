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