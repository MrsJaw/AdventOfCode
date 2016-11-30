using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string FilePath = string.Empty;
            string GiftDimensionsFilePath = string.Empty;
            int Result = 0;

            //Get Input
            Console.Write("Merry Christmas Elves! What's the file path for your gift dimensions?  ");
            GiftDimensionsFilePath = Console.ReadLine();

            //Calculate Gift Wrap
            if (!string.IsNullOrWhiteSpace(GiftDimensionsFilePath))
            {
                int RibbonResult = 0;
                Result = CalculateWrappingPaperFromDimensions(GiftDimensionsFilePath, out RibbonResult);
                if (Result > 0)
                {
                    Console.WriteLine(string.Format("You need {0} sq.ft. of wrapping paper and {1} feet of ribbon.", Result, RibbonResult));
                }
                else
                {
                    Console.WriteLine("Son of a nutcracker! Something's not right with those measurements.");
                }
            }
            else
            {
                Console.WriteLine("You cotten-headed ninnymuggins. That's not a file.");
            }

            Console.Read();
        }

        private static int CalculateWrappingPaperFromDimensions(string path, out int ribbonLength)
        {
            int Result = 0;
            ribbonLength = 0;

            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        while(!reader.EndOfStream)
                        {
                            string[] GiftDimensions = reader.ReadLine().Split('x');
                            if(GiftDimensions.Length == 3)
                            {
                                int Length = Convert.ToInt32(GiftDimensions[0]);
                                int Width = Convert.ToInt32(GiftDimensions[1]);
                                int Height = Convert.ToInt32(GiftDimensions[2]);
                                int AreaSide1 = Length * Width;
                                int AreaSide2 = Width * Height;
                                int AreaSide3 = Length * Height;
                                Result += (2 * AreaSide1) + (2 * AreaSide2) + (2 * AreaSide3) + Math.Min(AreaSide1, Math.Min(AreaSide2, AreaSide3));

                                //Part 2
                                int PerimeterSide1 = (2 * Length) + (2 * Width);
                                int PerimeterSide2 = (2 * Width) + (2 * Height);
                                int PerimeterSide3 = (2 * Length) + (2 * Height);
                                ribbonLength += Math.Min(PerimeterSide1, Math.Min(PerimeterSide2, PerimeterSide3)) + (Length * Width * Height);

                            }
                        }
                    }
                }
            }
            catch
            {
                Result = -1; // NEGATIVE SQUAREFOOTAGE OF WRAPPING PAPER. JUST GO WITH IT.
            }

            return Result;
        }
    }
}

