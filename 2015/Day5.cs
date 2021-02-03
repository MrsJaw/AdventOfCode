using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day5
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
                Console.WriteLine($"There are {Result} nice strings in the old way of thinking, but that was clearly ridiculous.");
                Result = EnlightenedMoralJudgementOfText(FilePath);
                Console.WriteLine($"Now we know better and there are {Result} nice strings.");
            }
            else
            {
                Console.WriteLine("The list itself is naughty so.....there's nothing to see here.");
            }

            Console.Write("Press any key to exit.");
            Console.Read();
        }

        
        private static int EnlightenedMoralJudgementOfText(string path)
        {
            int Result = 0;

            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string StringForJudgement = string.Empty;
                        while(!reader.EndOfStream)
                        {
                            StringForJudgement = reader.ReadLine();

                            /* Conditions for Niceness *
                             It contains a pair of any two letters that appears at least twice in the string 
                             without overlapping
                             It contains at least one letter which repeats with exactly one letter between them
                            */
                            bool Nice = false;
                            bool TheCouplesNoTouchyFactor = false;
                            bool TheSandwichFactor = false;

                            if(StringForJudgement.Length > 3)
                            {
                                for(int i = 0; i < StringForJudgement.Length-2 && !Nice; i++)
                                {
                                    TheSandwichFactor = TheSandwichFactor || (StringForJudgement[i] == StringForJudgement[i+2]);

                                    if(!TheCouplesNoTouchyFactor)
                                    {
                                        string CharPair = StringForJudgement.Substring(i, 2);
                                        string RemainingStringToCompare = StringForJudgement.Substring(i+2);
                                        TheCouplesNoTouchyFactor = RemainingStringToCompare.Contains(CharPair);
                                    }
                                    
                                    Nice = (TheSandwichFactor && TheCouplesNoTouchyFactor);
                                }
                            }

                            if(Nice)
                            {
                                Result ++;
                            }

                            //Output for testing
                            //Console.WriteLine($"{StringForJudgement}: NoTouchy? {TheCouplesNoTouchyFactor} Sandwich? {TheSandwichFactor} Nice? {Nice}");

                        }
                    }
                }
            }
            catch
            {
                //whomst among us can truly claim to be good?
            }

            return Result;
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
