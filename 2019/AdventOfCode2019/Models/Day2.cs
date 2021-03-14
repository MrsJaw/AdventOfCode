using System.ComponentModel.DataAnnotations;

namespace AdventOfCode2019.Models
{
    public class Day2
    {
        [Key]
        public int Id {get;set;}
        public int Day {get;set;}
        public string InputPath {get;set;}
        public string FirstStarResult {get;set;}
        public string SecondStarResult { get; set; }
    }
}