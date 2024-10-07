namespace WordFinderApp.ConsoleApp;

using WordFinderApp.Logic;

static class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <matrix> <wordstream>");
            return;
        }

        var separator = ',';
        
        Console.WriteLine("Initializing Matrix");
        var matrix = ParseArgument(args[0], separator);
        IWordFinder wordFinder = new WordFinder(matrix);
        
        Console.WriteLine("Finding words");
        var wordStream = ParseArgument(args[1], separator);
        var foundWords = wordFinder.Find(wordStream);

        Console.WriteLine("Founded words");
        foreach (var word in foundWords)
        {
            Console.WriteLine(word);
        }
    }

    private static IEnumerable<string> ParseArgument(string argument, char separator)
    {
        return argument.Split(separator);
    }
}