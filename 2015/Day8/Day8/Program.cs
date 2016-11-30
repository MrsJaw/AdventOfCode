using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string FilePath = string.Empty;
            int Result = -1;

            //Read Input From File
            Console.Write("Merry Christmas Santa! What's the file path of your list?  ");
            FilePath = Console.ReadLine();

            if(!string.IsNullOrWhiteSpace(FilePath))
            {
                Result = CountFileCharacters(FilePath);
                Console.WriteLine(string.Format("{0} is the number of characters you're looking for.", Result));                                
            }
            else
            {
                Console.WriteLine("nooooooooooope");
            }

            Console.Read();

        }

        private static int CountFileCharacters(string filePath)
        {
            string Line = string.Empty;
            int PrintedCharsInLine = 0;
            int TotalPrintedChars = 0;
            int TotalChars = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    Line = reader.ReadLine();
                    TotalChars += Line.Count();
                    PrintedCharsInLine = Regex.Unescape(Line).Count();
                    if (Line.StartsWith("\""))
                    {
                        PrintedCharsInLine--;
                    }
                    if(Line.EndsWith("\""))
                    {
                        PrintedCharsInLine--;
                    }
                    TotalPrintedChars += PrintedCharsInLine;
                }
            }
            return TotalChars - TotalPrintedChars;
        }
    }
}
