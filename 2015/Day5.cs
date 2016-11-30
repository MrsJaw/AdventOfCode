using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoCDay5
{
    class Program
    {
        static void Main(string[] args)
        {
            string FilePath = string.Empty;
            string SantasDirections = string.Empty;
            int Result = 0;

            //Get Input
            Console.Write("Merry Christmas Santa! What's the path of the file that's ready for judgement?  ");
            FilePath = Console.ReadLine();

            //Count Floors
            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                Result = MoralJudgementOfText(FilePath);
                Console.WriteLine("There are " + Result + " nice strings");
            }
            else
            {
                Console.WriteLine("The list itself is naughty so.....there's nothing to see here.");
            }

            Console.Read();
        }

        private static int MoralJudgementOfText(string path)
        {
            int Result = 0;

            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    Regex Vowels = new Regex("[aeiou]", RegexOptions.Compiled); //looks for those characters
                    Regex DoubleLetters = new Regex("(\\w)\\1", RegexOptions.Compiled); //(\w) takes a letter and stores it \1 matches it against the stored letter
                    Regex NaughtyStrings = new Regex("(ab|cd|pq|xy)", RegexOptions.Compiled); //looks for any of those strings
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string StringForJudgement = string.Empty;
                        while(!reader.EndOfStream)
                        {
                            StringForJudgement = reader.ReadLine();

                            /* Conditions for Niceness *
                             It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
                             It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
                             It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.*/
                            if(!string.IsNullOrWhiteSpace(StringForJudgement) &&
                                Vowels.Matches(StringForJudgement).Count >= 3 &&
                                DoubleLetters.Matches(StringForJudgement).Count > 0 &&
                                NaughtyStrings.Matches(StringForJudgement).Count == 0 
                               )
                            {
                                Result ++;
                            }

                        }
                    }
                }
            }
            catch
            {
                //pretend nothing happened, because that's totally a great idea.
            }

            return Result;
        }
    }
}
