using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputKey = string.Empty;
            string InputNumberOf0s = string.Empty;
            int Result = 0;
            int NumberOf0s = 0;

            //Get Input
            Console.Write("Merry Christmas Santa! What's the super secret input key?  ");
            InputKey = Console.ReadLine();
            Console.Write("How many zeros do you want in your hash?  ");
            InputNumberOf0s = Console.ReadLine();

            //Count Floors
            if (!string.IsNullOrWhiteSpace(InputKey) && !string.IsNullOrWhiteSpace(InputNumberOf0s) && Int32.TryParse(InputNumberOf0s, out NumberOf0s))
            {
                Result = ComputeNumberFromSuperSecretKey(InputKey, NumberOf0s);
                Console.WriteLine("Result: " + Result);
            }
            else
            {
                Console.WriteLine("wuuuuuuuuuut are you even doing");
            }

            Console.Read();
        }

        private static int ComputeNumberFromSuperSecretKey(string inputKey, int numberOf0s)
        {
            int Result = 0;
            string CalculatedHash = CalculateMD5Hash(inputKey + Result.ToString());
            string HashCompare = new string('0', numberOf0s);

            while(!CalculatedHash.StartsWith(HashCompare))
            {
                Result++;
                CalculatedHash = CalculateMD5Hash(inputKey + Result.ToString());
            }

            return Result;
        }

        private static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            string[] HexTbl = Enumerable.Range(0, 256).Select(v => v.ToString("X2")).ToArray();
        
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            foreach (var i in hash)
            {
                sb.Append(HexTbl[i]);
            }
            return sb.ToString();
        }
    }
}
