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
                Result = CalculateWrappingPaperFromDimensions(GiftDimensionsFilePath);
                if (Result > 0)
                {
                    Console.WriteLine("You need " + Result + " sq.ft. of wrapping paper.");
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

        private static int CalculateWrappingPaperFromDimensions(string path)
        {
            int Result = 0;

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
                                int Lenth = Convert.ToInt32(GiftDimensions[0]);
                                int Width = Convert.ToInt32(GiftDimensions[1]);
                                int Height = Convert.ToInt32(GiftDimensions[2]);
                                int Side1 = Lenth * Width;
                                int Side2 = Width * Height;
                                int Side3 = Lenth * Height;
                                Result += (2 * Side1) + (2 * Side2) + (2 * Side3) + Math.Min(Side1, Math.Min(Side2, Side3));
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

