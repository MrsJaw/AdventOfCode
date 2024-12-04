using System.Linq;
using System.Text.RegularExpressions;

namespace Day2;

class Program
{
    public static string _mulInstructionPattern = @"mul\(\d{1,3},\d{1,3}\)";
    public static string _factorPattern = @"\d{1,3}";

    static void Main(string[] args)
    {
        string? FilePath = string.Empty;
        int Result = 0;

        //Get Input
        Console.Write("Hello Shop Keep! What's the file path of your weird computer program?  ");
        FilePath = Console.ReadLine();
        var input = GetInputFromFile(FilePath);

        //Validate Input
        if (!string.IsNullOrWhiteSpace(input))
        {
            //solve the puzzle part 1
            Result = SolveFirstStarPuzzle(input);

            Console.WriteLine($"I solved the puzzle! The answer is {Result}");

            var BonusResult = SolveSecondStarPuzzle(input);

            Console.WriteLine($"I solved the next puzzle! The answer is {BonusResult}");
        }
        else
        {
            Console.WriteLine("Your Chief Historian is in another castle!");
        }

        Console.Read();
    }

    private static string GetInputFromFile(string? path)
    {
        string Result = string.Empty;

        try
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Result = reader.ReadToEnd();
                }
            }

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        return Result;

    }

    private static int SolveFirstStarPuzzle(string input)
    {
        int Result = 0;
        var matches = Regex.Matches(input, _mulInstructionPattern);

        foreach (Match match in matches)
        {
            var factors = Regex.Matches(match.Value, _factorPattern).Select(x => Convert.ToInt32(x.Value)).ToArray();
            Result += factors[0] * factors[1];
        }

        return Result;
    }

    private static int SolveSecondStarPuzzle(string input)
    {
        int Result = 0;

        var doPattern = $"do(?!n)";
        var dont = "don't";
        var start = 0;
        var stop = input.IndexOf(dont);
        var chunk = input;

        while(stop > -1 && stop < input.Length)
        {
            chunk = input.Substring(start, (stop - start) + dont.Length);
            Result += SolveFirstStarPuzzle(chunk); 
            start = Regex.Match(input.Substring(stop), doPattern).Index + stop;
            stop = input.IndexOf(dont, start);
        }

        chunk = input.Substring(start);
        Result += SolveFirstStarPuzzle(chunk);

        return Result;
    }

} 
