using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    class Program
    {
        private struct Coordinate
        {
            public int X;
            public int Y;
        }

        static void Main(string[] args)
        {
            string FilePath = string.Empty;
            string SantasDirections = string.Empty;
            int Result = 0;

            //Get Input
            Console.Write("Merry Christmas Santa! What's the file path of your directions?  ");
            FilePath = Console.ReadLine();

            //Count Floors
            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                Result = AnalyzeDirectionsFromFile(FilePath);
                Console.WriteLine("This is the wrong Trelew! " + Result + " houses got gifts.");
            }
            else
            {
                Console.WriteLine("You've got no directions. You'll just have to go door to door and hope you find Natalie.");
            }

            Console.Read();
        }

        private static int AnalyzeDirectionsFromFile(string path)
        {
            int Result = 0;
            Coordinate CurrentCoordinates = new Coordinate() { X = 0, Y = 0 } ;
            Coordinate RoboCurrentCoordinates = new Coordinate() { X = 0, Y = 0 };
            Dictionary<Coordinate, int> PresentsDeliveredByHouse = new Dictionary<Coordinate, int>();

            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        char NextDirection = '0';
                        PresentsDeliveredByHouse.Add(CurrentCoordinates, 1);

                        //Part 2
                        PresentsDeliveredByHouse[RoboCurrentCoordinates]++;
                        int NextDirectionCount = 0;

                        while (!reader.EndOfStream)
                        {
                            NextDirection = Convert.ToChar(reader.Read());
                            NextDirectionCount++;
                            if (NextDirectionCount%2==0)
                            {
                                MoveSanta(NextDirection, ref CurrentCoordinates, ref PresentsDeliveredByHouse);
                            }
                            else
                            {
                                MoveSanta(NextDirection, ref RoboCurrentCoordinates, ref PresentsDeliveredByHouse);
                            }

                        }
                    }

                    Result = PresentsDeliveredByHouse.Count(); 
                }
            }
            catch(Exception ex)
            {
                //pretend nothing happened, because that's totally a great idea.
            }

            return Result;
        }

        private static void MoveSanta(char nextDirection, ref Coordinate coordinates, ref Dictionary<Coordinate, int> presentsDeliveredByHouse)
        {
            switch (nextDirection)
            {
                case '^':
                    coordinates.X++;
                    break;
                case 'v':
                    coordinates.X--;
                    break;
                case '>':
                    coordinates.Y++;
                    break;
                case '<':
                    coordinates.Y--;
                    break;
            }

            if (!presentsDeliveredByHouse.ContainsKey(coordinates))
            {
                presentsDeliveredByHouse.Add(coordinates, 1);
            }
            else
            {
                presentsDeliveredByHouse[coordinates]++;
            }
        }
    }
}
