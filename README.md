# HackerRankTemplate
Visual Studio template for doing HackerRank projects in C#

At some point it would be nice to have the actual template file in here. For now you have two options for use:

1. Grab the project, load it into Visual Studio, then choose File -> Export Template. If you choose to import the template immediately
after export, you'll be able to create a new project immediately.
2. Alternately, create your own Console application, then just copy the contents of Program.cs into your file and go from there.

The program has one stub method called *SolvePuzzle* which takes an IReaderWriter parameter. Use the IReaderWriter methods to interact with the
console. 

When running, the boilerplate will detect whether a debugger is attached. If not, a ConsoleReaderWriter will be used to manage
interactions with the console. If a debugger IS attached, a TextFileReaderWriter will be used instead. The TextFileReaderWriter:
* Reads input from a file called *input.txt* and
* Optionally, compares your outputs line-by-line to a file called *expectedOutput.txt*

This makes it easy to debug test cases from HackerRank.

When everything looks good, copy your entire Program.cs file into the HackerRank text box. As long as you didn't get all fancy by putting your helper
classes in their own files (the horror!) it should Just Work.