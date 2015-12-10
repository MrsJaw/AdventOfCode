using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {

        private static Dictionary<Point, bool> _LightDisplay = new Dictionary<Point, bool>();
      

        static void Main(string[] args)
        {
            string FilePath = string.Empty;
            int Result = 0;
            //Get Input
            Console.Write("Give me the location of the lighting instructions.  ");
            FilePath = Console.ReadLine();

            //Count Floors
            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                Result = SetUpLights(FilePath);
                Console.WriteLine(Result + " lights are on.");
                Console.WriteLine("Would you like to see the design? (Y/N) ");
                char GetDesignResponse = Convert.ToChar(Console.Read());
                if(GetDesignResponse == 'Y' || GetDesignResponse == 'y')
                {
                    LightItUp(FilePath);
                    Console.WriteLine("The design is saved next to your instructions!");
                }
            }
            else
            {
                Console.WriteLine("The grinch stole your lights display...AND the roast beast.");
            }

            Console.Read();
        }

        private static void LightItUp(string path)
        {
            int IndexOfLastSlash = path.LastIndexOf('/');
            string NewPath = path.Substring(0, IndexOfLastSlash + 1) + "FinishedDisplay.txt";
        }

        private static int SetUpLights(string path)
        {
            int Result = 0;

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    InitializeLightDisplay();

                    while (!reader.EndOfStream)
                    {
                        string Instruction = reader.ReadLine();
                        string[] NumberStrings = Regex.Split(Instruction, @"\D+");
                        int[] Numbers = NumberStrings.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => Convert.ToInt32(s)).ToArray(); //splits on one or more digit numbers
                        if (Numbers.Length == 4)
                        {
                            Point CurrentLocation = new Point(Numbers[0], Numbers[1] );
                            for(int row = Numbers[0]; row <= Numbers[2]; row ++)
                            {
                                CurrentLocation.X = row;
                                for(int col = Numbers[1]; col <= Numbers[3]; col ++)
                                {
                                    CurrentLocation.Y = col;
                                    if(Instruction.StartsWith("turn off"))
                                    {
                                        _LightDisplay[CurrentLocation] = false;
                                    }
                                    else if(Instruction.StartsWith("turn on"))
                                    {
                                        _LightDisplay[CurrentLocation] = true;
                                    }
                                    else if(Instruction.StartsWith("toggle"))
                                    {
                                        _LightDisplay[CurrentLocation] = !_LightDisplay[CurrentLocation];
                                    }
                                }
                            }
                        }
                    }

                    Result = _LightDisplay.Values.Where(x => x == true).Count();
                }
                
            }
            catch
            {
                //pretend nothing happened, because that's totally a great idea.
            }

            return Result;
        }

        private static void InitializeLightDisplay()
        {
            for(int row = 0; row < 1000; row++)
            {
                for(int col = 0; col < 1000; col++)
                {
                    Point light = new Point(row, col);
                    _LightDisplay.Add(light, false);
                }
            }
        }
    }
}

