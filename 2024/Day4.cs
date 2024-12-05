namespace Day4;

class Program
{
    record Input
    {
        public char[][] WordSearch { get; init; } 
    }

    public enum Direction
    {
        Right,
        Left,
        Up,
        Down,
        UpRightDiagonal,
        UpLeftDiagonal,
        DownRightDiagonal,
        DownLeftDiagonal
    }

    static void Main(string[] args)
    {
        string? FilePath = string.Empty;
        int Result = 0;

        //Get Input
        Console.Write("Hello! What's the file path of your word search?  ");
        FilePath = Console.ReadLine();
        var input = GetInputFromFile(FilePath);

        //Validate Input
        if (input.WordSearch.Length > 0)
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
        var input = new List<char[]>();

        try
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine()!;
                        input.Add(line.ToCharArray());
                    }
                }
            }

            return new() { WordSearch = input.ToArray()};
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return new();

    }

    private static int SolveFirstStarPuzzle(Input input)
    {
        int Result = 0;
        var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();
        for (int i = 0; i < input.WordSearch.Length; i++)
        {
            var row = input.WordSearch[i];
            for(int j = 0; j < row.Length; j++)
            {
                var letter = row[j];
                if(letter == 'X')
                {
                    foreach(var direction in directions)
                    {
                        if(CheckNextLetter('M', direction, j, i, input.WordSearch))
                        {
                            var nextX = GetNextX(direction, j);
                            var nextY = GetNextY(direction, i);
                           if(CheckNextLetter('A', direction, nextX, nextY, input.WordSearch))
                            {
                                var nextNextX = GetNextX(direction, nextX);
                                var nextNextY = GetNextY(direction, nextY);
                                if(CheckNextLetter('S', direction, nextNextX, nextNextY, input.WordSearch))
                                {
                                    Result++;
                                }
                            }
                        }
                    }
                }
            }
        }
        return Result;
    }

    private static int GetNextX(Direction direction, int currentX)
    {
        return direction switch
        {
            Direction.Up => currentX,
            Direction.Down => currentX,
            Direction.Left => --currentX,
            Direction.UpLeftDiagonal => --currentX,
            Direction.DownLeftDiagonal => --currentX,
            Direction.Right => ++currentX,
            Direction.UpRightDiagonal => ++currentX,
            Direction.DownRightDiagonal => ++currentX,
            _ => -1
        };
    }

    private static int GetNextY(Direction direction, int currentY)
    {
        return direction switch
        {
            Direction.Up => --currentY,
            Direction.UpLeftDiagonal => --currentY,
            Direction.UpRightDiagonal => --currentY,
            Direction.Down => ++currentY,
            Direction.DownLeftDiagonal => ++currentY,
            Direction.DownRightDiagonal => ++currentY,
            Direction.Left => currentY,
            Direction.Right => currentY,
            _ => -1
        };
    }

    private static bool CheckNextLetter(char letter, Direction direction, int currentX, int currentY, char[][] map)
    {
        int nextX = GetNextX(direction, currentX);

        int nextY = GetNextY(direction, currentY);

        if (nextY >= 0 && nextY < map.Length)
        {
            var row = map[nextY];
            if (nextX >= 0 && nextX < row.Length)
            {
                return row[nextX] == letter;
            }
        }

        return false;
    }

    private static int SolveSecondStarPuzzle(Input input)
    {
        int Result = 0;

        for (int i = 0; i < input.WordSearch.Length; i++)
        {
            var row = input.WordSearch[i];
            for (int j = 0; j < row.Length; j++)
            {
                var letter = row[j];
                if (letter == 'A')
                {
                    var DownRight = CheckNextLetter('M', Direction.UpLeftDiagonal, j, i, input.WordSearch) && CheckNextLetter('S', Direction.DownRightDiagonal, j, i, input.WordSearch);
                    var UpLeft = CheckNextLetter('M', Direction.DownRightDiagonal, j, i, input.WordSearch) && CheckNextLetter('S', Direction.UpLeftDiagonal, j, i, input.WordSearch);
                    var UpRight = CheckNextLetter('M', Direction.UpRightDiagonal, j, i, input.WordSearch) && CheckNextLetter('S', Direction.DownLeftDiagonal, j, i, input.WordSearch);
                    var DownLeft = CheckNextLetter('M', Direction.DownLeftDiagonal, j, i, input.WordSearch) && CheckNextLetter('S', Direction.UpRightDiagonal, j, i, input.WordSearch);
                    if ((DownRight || UpLeft) && (UpRight || DownLeft))
                    {
                        Result++; 
                    }
                }
            }
        }

        return Result;
    }

} 
