using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AdventOfCode2019.Models
{
    public class Puzzle
    {
        private string _Input = string.Empty;

        [Key]
        public int Id {get;set;}
        public int Day {get;set;}
        public string Input 
        {
            get 
            {
                if(string.IsNullOrEmpty(_Input))
                {
                    _Input = ReadInputFromFile();
                }
                return _Input;
            }
        }
        public string InputPath {get;set;}
        public string FirstStarResult {get;set;}
        public string SecondStarResult { get; set; }

        
         private  string ReadInputFromFile()
        {
            string Result = string.Empty;

            if(!string.IsNullOrWhiteSpace(InputPath))
            {
                using (StreamReader reader = new StreamReader(InputPath))
                {
                    Result = reader.ReadToEnd();
                }
            }            

            return Result;
        }

        
    }
}