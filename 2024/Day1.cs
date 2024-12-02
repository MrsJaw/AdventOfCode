namespace Day1;

class Program
{
    record InputLists
    {
        public List<int> List1 { get; init; }
        public List<int> List2 { get; init; }
    }

    static void Main(string[] args)
    {
        string? FilePath = string.Empty;
        int Result = 0;

        //Get Input
        Console.Write("Hello Historians! What's the file path of your lists?  ");
        FilePath = Console.ReadLine();
        var input = GetInputFromFile(FilePath);

        //Validate Input
        if (input.List1.Count > 0 && input.List1.Count == input.List2.Count)
        {
            //solve the puzzle part 1
            Result = SolveFirstStarPuzzle(input);

            Console.WriteLine($"I solved the puzzle! The answer is {Result}");

            var BonusResult = SolveSecondStarPuzzle(input);

            Console.WriteLine($"I solved the next puzzle! The answer is {BonusResult}");
        }
        else
        {
            Console.WriteLine("Your Head Historian is in another castle!");
        }

        Console.Read();
    }

    private static InputLists GetInputFromFile(string? path)
    {
        string Result = string.Empty;
        List<int> InputColumn1 = new List<int>();
        List<int> InputColumn2 = new List<int>();

        try
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] locations = reader.ReadLine()!.Split("   ");
                        InputColumn1.Add(Convert.ToInt32(locations[0]));
                        InputColumn2.Add(Convert.ToInt32(locations[1]));
                    }
                }
            }

            return new InputLists { List1 = InputColumn1, List2 = InputColumn2 };
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        return new();
    }

    private static int SolveFirstStarPuzzle(InputLists input)
    {
        int Result = 0;

        var orderedList1 = input.List1.Order();
        var orderedList2 = input.List2.Order();

        int i = 0;
        foreach (var item in orderedList1)
        {
            var item2 = orderedList2.ElementAt(i);
            Result += Math.Abs(item-item2);
            i++;
        }

        return Result;
    }

    private static int SolveSecondStarPuzzle(InputLists input)
    {
        int Result = 0;

        foreach(var item in input.List1)
        {
            Result += item * input.List2.Count(x => x == item);
        }

        return Result;
    }

}
