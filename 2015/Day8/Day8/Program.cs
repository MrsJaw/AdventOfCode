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
        public struct CharacterCounts {
                public int CodeCharacters;
                public int InMemoryChraacters;
                public int EncodedCharacters;
            };

        static void Main(string[] args)
        {
            string FilePath = string.Empty;
            CharacterCounts Result = new CharacterCounts();

            //Read Input From File
            Console.Write("Merry Christmas Santa! What's the file path of your list?  ");
            FilePath = Console.ReadLine();

            try
            {
                Result = CountFileCharacters(FilePath);
                Console.WriteLine(string.Format("{0} is the number of characters you're looking for.", (Result.CodeCharacters - Result.InMemoryChraacters)));                                
                Console.WriteLine(string.Format("{0} is the number of encoded characters you're looking for.", (Result.EncodedCharacters - Result.CodeCharacters)));                                
            }
            catch(Exception ex)
            {
                Console.WriteLine("nooooooooooope");
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press any key to exit");
            Console.Read();

        }

        private static CharacterCounts CountFileCharacters(string filePath)
        {
            string Line = string.Empty;
            int PrintedCharsInLine = 0;
            int EncodedCharsInLine = 0;
            int TotalEncodedChars = 0;
            int TotalPrintedChars = 0;
            int TotalChars = 0;
            CharacterCounts Result = new CharacterCounts();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    Line = reader.ReadLine();
                    TotalChars += Line.Count();
                    PrintedCharsInLine = Regex.Unescape(Line).Count();
                    EncodedCharsInLine = Regex.Escape(Line).Count();
                    if (Line.StartsWith("\""))
                    {
                        PrintedCharsInLine--;
                        EncodedCharsInLine+=2;
                        Line = Line.Substring(1);
                    }
                    if(Line.EndsWith("\""))
                    {
                        PrintedCharsInLine--;
                        EncodedCharsInLine+=2;
                        Line = Line.Substring(0, Line.Length-1);
                    }            
                    EncodedCharsInLine += (Line.Count() - Line.Replace("\"", string.Empty).Count());        
                    TotalPrintedChars += PrintedCharsInLine;
                    TotalEncodedChars += EncodedCharsInLine;
                }
            }
            Result.CodeCharacters = TotalChars;
            Result.InMemoryChraacters = TotalPrintedChars;
            Result.EncodedCharacters = TotalEncodedChars;
            return Result;
        }
    }
}
