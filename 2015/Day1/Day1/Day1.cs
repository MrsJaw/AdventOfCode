using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Program
    {
        static void Main(string[] args)
        {
            string FilePath = string.Empty;
            string SantasDirections = string.Empty;
            int Result = 0;

            //Get Input
            Console.Write("Merry Christmas Santa! What's the file path of your directions?  ");
            FilePath = Console.ReadLine();
            SantasDirections = GetInputFromFile(FilePath);

            //Count Floors
            if(!string.IsNullOrWhiteSpace(SantasDirections))
            {
                Result = SantasDirections.Count(x => x.Equals('(')) - SantasDirections.Count(x => x.Equals(')'));
                Console.WriteLine("You need to go to floor " + Result);

                //Part 2
                int floor = 0;
                int pos = 0;
                foreach (char step in SantasDirections)
                {
                    pos++;
                    switch(step)
                    {
                        case '(':
                            floor++;
                            break;
                        case ')':
                            floor--;
                            break;
                    }

                    if(floor < 0)
                    {
                        break;
                    }
                }

                Console.WriteLine(string.Format("At position {0} you ended up in the basement", pos));
            }
            else
            {
                Console.WriteLine("Sorry Santa, you've got bad directions. Just leave those presents for me.");
            }

            Console.Read();
        }

        private static string GetInputFromFile(string path)
        {
            string Result = string.Empty;

            try
            {
                if(!string.IsNullOrWhiteSpace(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        Result = reader.ReadToEnd();
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
