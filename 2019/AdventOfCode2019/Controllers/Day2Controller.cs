using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdventOfCode2019.Models;

namespace AdventOfCode2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Day2Controller : ControllerBase
    {
        private readonly PuzzleContext _context;

        public Day2Controller(PuzzleContext context)
        {
            _context = context;
        }
        
        // GET: api/Day2/
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Day2>>> GetDay2()
        {
            return await _context.Puzzles
                                 .Where(x => x.Day == 2)
                                 .Select(x => ItemToDTO(x))
                                 .ToListAsync();
        }

        // GET: api/Day2/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Day2>> GetDay2(int id)
        {
            var Day2 = await _context.Puzzles.FindAsync(id);

            if (Day2 == null)
            {
                return NotFound();
            }

            return ItemToDTO(Day2);
        }

        // POST: api/Day2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Day2>> PostDay2(Day2 Day2)
        {
            var puzzle = await _context.Puzzles.FindAsync(Day2.Id);
            if (puzzle != null)
            {
                _context.Puzzles.Remove(puzzle);
                await _context.SaveChangesAsync();
            }
            
            puzzle = new Puzzle(){  Day = 2,
                                    InputPath = Day2.InputPath,
                                    FirstStarResult = Day2.FirstStarResult,
                                    SecondStarResult = Day2.SecondStarResult
                                };
            _context.Puzzles.Add(puzzle);            
            await _context.SaveChangesAsync();
            
            SolveDay2(ref puzzle);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDay2", new { id = puzzle.Day }, ItemToDTO(puzzle));
        }

        private void SolveDay2(ref Puzzle puzzle)
        {
            int[] IntcodePart1 = CreateIntcode(puzzle.Input, 12, 2);
            puzzle.FirstStarResult = IntcodePart1.FirstOrDefault().ToString();

            bool Continue = true;
            int noun = 0;
            int verb = 0;
            for(noun = 0; noun < 100 && Continue; noun ++)
            {
                for(verb = 0; verb < 100 && Continue; verb++)
                {
                    if(CreateIntcode(puzzle.Input, noun, verb).FirstOrDefault() == 19690720)
                    {                        
                        puzzle.SecondStarResult = (100 * noun + verb).ToString();
                        Continue = false;
                    }
                }
            }

        }

        private int[] CreateIntcode(string input, int noun, int verb)
        {
            int[] Intcode = input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToArray();
            Intcode[1] = noun;
            Intcode[2] = verb; 
            bool Continue = true;
            int Index = 0;
            
            while(Continue && Index < Intcode.Length-1)
            {
                int IntOperation = Intcode[Index];
                switch (IntOperation)
                {
                    case 1 :
                        int NewSum = Intcode[Intcode[++Index]] + Intcode[Intcode[++Index]];
                        Intcode[Intcode[++Index]] = NewSum;
                        break;
                    case 2 :                    
                        int NewProduct = Intcode[Intcode[++Index]] * Intcode[Intcode[++Index]];
                        Intcode[Intcode[++Index]] = NewProduct;
                        break;
                    case 99 :
                        Continue = false;
                        break;
                    default:
                        Index++;
                        break;
                }             
            }
            return Intcode;
        }

        private bool Day2Exists(int id)
        {
            return _context.Puzzles.Any(e => e.Id == id);
        }
        
        private static Day2 ItemToDTO(Puzzle puzzle) => new Day2
        {
            Id = puzzle.Id,
            Day = puzzle.Day,
            InputPath = puzzle.InputPath,
            FirstStarResult = puzzle.FirstStarResult,
            SecondStarResult = puzzle.SecondStarResult
        };
    }
}
