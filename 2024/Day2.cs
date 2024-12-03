using System.Linq;

namespace Day2;

class Program
{
    record Input
    {
        public List<int[]> Reports { get; init; }
    }

    static void Main(string[] args)
    {
        string? FilePath = string.Empty;
        int Result = 0;

        //Get Input
        Console.Write("Hello Engineers! What's the file path of your reports?  ");
        FilePath = Console.ReadLine();
        var input = GetInputFromFile(FilePath);

        //Validate Input
        if (input.Reports.Count > 0)
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

    private static Input GetInputFromFile(string? path)
    {
        string Result = string.Empty;
        Input result = new() { Reports = new()};

        try
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] locations = reader.ReadLine()!.Split(" ", options: StringSplitOptions.RemoveEmptyEntries);
                        result.Reports.Add(locations.Select(x => Convert.ToInt32(x)).ToArray());
                    }
                }
            }

            return result;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        return new();
    }

    private static int SolveFirstStarPuzzle(Input input)
    {
        return input.Reports.Count(x => EvaluateReport(x));
    }

    private static int SolveSecondStarPuzzle(Input input)
    {
        int Result = 0;

        foreach (var report in input.Reports)
        {
            var safe = EvaluateReport(report);
            for (int i = 0; i < report.Length && !safe; i++)
            {
                var dampenedReport = report.ToList();
                dampenedReport.RemoveAt(i);
                safe = EvaluateReport(dampenedReport.ToArray());
            }
            if (safe)
            {
                Result++;
            }
        }

        return Result;
    }

    private static bool EvaluateReport(int[] report)
    {
        var MinDifference = 1;
        var MaxDifference = 3;

        if (report[0] > report[1])
        {
            MinDifference = -3;
            MaxDifference = -1;
        }

        var Safe = true;
        for (int i = 0; i < report.Length - 1 && Safe; i++)
        {
            var difference = report[i + 1] - report[i];
            Safe = difference >= MinDifference && difference <= MaxDifference;
        }

        return Safe;
    }

}
