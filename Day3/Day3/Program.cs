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
            Dictionary<Coordinate, int> PresentsDeliveredByHouse = new Dictionary<Coordinate, int>();

            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        char NextDirection = '0';
                        PresentsDeliveredByHouse.Add(CurrentCoordinates, 1);

                        while (!reader.EndOfStream)
                        {
                            NextDirection = Convert.ToChar(reader.Read());
                            switch(NextDirection)
                            {
                                case '^':
                                    CurrentCoordinates.X++;
                                    break;
                                case 'v':
                                    CurrentCoordinates.X--;
                                    break;
                                case '>':
                                    CurrentCoordinates.Y++;
                                    break;
                                case '<':
                                    CurrentCoordinates.Y--;
                                    break;
                            }

                            if (!PresentsDeliveredByHouse.ContainsKey(CurrentCoordinates))
                            {
                                PresentsDeliveredByHouse.Add(CurrentCoordinates, 1);
                            }
                            else
                            {
                                PresentsDeliveredByHouse[CurrentCoordinates]++;
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
    }
}
