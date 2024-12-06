namespace Day4;

class Program
{
    struct Order
    {
        public int Before = 0;
        public int After = 0;

        public Order(int before, int after)
        {
            Before = before;
            After = after;
        }
    }

    record Input
    {
        public List<Order> OrderingRules { get; set; } = new();
        public List<int[]> Updates { get; set; } = new();
    }

    static void Main(string[] args)
    {
        string? FilePath = string.Empty;
        int Result = 0;

        //Get Input
        Console.Write("Hello! What's the file path of your print instructions?  ");
        FilePath = Console.ReadLine();
        var input = GetInputFromFile(FilePath);

        //Validate Input
        if (input.OrderingRules.Count > 0 && input.Updates.Count > 0)
        {
            //solve the puzzle part 1
            Result = SolveFirstStarPuzzle(input);

            Console.WriteLine($"I solved the puzzle! The answer is {Result}");
        }
        else
        {
            Console.WriteLine("Your Chief Historian is in another castle!");
        }

        Console.Read();
    }

    private static Input GetInputFromFile(string? path)
    {
        var Result = new Input();

        try
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine()!;

                        string[] orderedPages = line.Split("|");
                        if (orderedPages.Length > 1)
                        {
                            Result.OrderingRules.Add(new(Convert.ToInt32(orderedPages[0]), Convert.ToInt32(orderedPages[1])));
                        }

                        string[] updatePages = line.Split(",");
                        if(updatePages.Length > 1)
                        {
                            Result.Updates.Add(updatePages.Select(x => Convert.ToInt32(x)).ToArray());
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return Result;

    }

    private static int SolveFirstStarPuzzle(Input input)
    {
        int Result = 0;

        foreach (var update in input.Updates)
        {
            var valid = true;
            for (int i = 0; i < update.Length && valid; i++)
            {
                valid = false;
                var page = update[i];
                var allPriorPages = update[0..i];
                var pagesThatMustFollow = GetPagesThatFollow(page, input.OrderingRules);
                if (allPriorPages == null || allPriorPages.Length == 0 || allPriorPages.Intersect(pagesThatMustFollow)?.Any() != true)
                {
                    var allSucceedingPages = update[i..update.Length];
                    var pagesThatMustPrecede = GetPagesThatPrecede(page, input.OrderingRules);
                    if (allSucceedingPages == null || allSucceedingPages.Length == 0 || allSucceedingPages.Intersect(pagesThatMustPrecede)?.Any() != true)
                    {
                        valid = true;
                    }
                }
            }
            if(valid)
            {
                Result += update[update.Length/2];
            }
        }
        return Result;
    }

    private static List<int> GetPagesThatFollow(int page, List<Order> orders) => 
        orders.Where(x => x.Before == page).Select(x => x.After).ToList();
    
    private static List<int> GetPagesThatPrecede(int page, List<Order> orders) => 
        orders.Where(x => x.After == page).Select(x => x.Before).ToList();

} 
