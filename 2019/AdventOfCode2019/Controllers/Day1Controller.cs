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
    public class Day1Controller : ControllerBase
    {
        private readonly PuzzleContext _context;

        public Day1Controller(PuzzleContext context)
        {
            _context = context;
        }

        // GET: api/Day1/
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Day1>>> GetDay1()
        {
            return await _context.Puzzles
                                 .Where(x => x.Day == 1)
                                 .Select(x => ItemToDTO(x))
                                 .ToListAsync();
        }

        // GET: api/Day1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Day1>> GetDay1(int id)
        {
            var Day1 = await _context.Puzzles.FindAsync(id);

            if (Day1 == null)
            {
                return NotFound();
            }

            return ItemToDTO(Day1);
        }

        // POST: api/Day1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Day1>> PostDay1(Day1 Day1)
        {
            var puzzle = await _context.Puzzles.FindAsync(Day1.Id);
            if (puzzle != null)
            {
                _context.Puzzles.Remove(puzzle);
                await _context.SaveChangesAsync();
            }
            
            puzzle = new Puzzle(){  Day = 1,
                                    InputPath = Day1.InputPath,
                                    FirstStarResult = Day1.FirstStarResult,
                                    SecondStarResult = Day1.SecondStarResult
                                };
            _context.Puzzles.Add(puzzle);            
            await _context.SaveChangesAsync();
            
            SolveDay1(ref puzzle);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDay1", new { id = puzzle.Id }, ItemToDTO(puzzle));
        }

        private void SolveDay1(ref Puzzle puzzle)
        {
            //Read Input    
            IEnumerable<int> InputNumbers = puzzle.Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s));
    
            //Solve First Star
            puzzle.FirstStarResult = InputNumbers.Select(n => CalculateFuelForMass(n)).Sum().ToString();

            //Solve Second Star
            int SecondStarResult = 0;
            Parallel.ForEach(InputNumbers, n =>
            {
                int FuelForNumber = 0;
                while(n > 0)
                {
                    n = CalculateFuelForMass(n);
                    if(n > 0)
                    {
                        FuelForNumber += n;
                    }
                }
                SecondStarResult += FuelForNumber;
            });

            puzzle.SecondStarResult = SecondStarResult.ToString();
        }

        private int CalculateFuelForMass(int mass)
        {
            return Convert.ToInt32(Math.Floor(mass/3M)) - 2;
        }

        private bool Day1Exists(int id)
        {
            return _context.Puzzles.Any(e => e.Id == id);
        }
        
        private static Day1 ItemToDTO(Puzzle puzzle) => new Day1
        {
            Id = puzzle.Id,
            Day = puzzle.Day,
            InputPath = puzzle.InputPath,
            FirstStarResult = puzzle.FirstStarResult,
            SecondStarResult = puzzle.SecondStarResult
        };
    }
}
