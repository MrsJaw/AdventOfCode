using Microsoft.EntityFrameworkCore;

namespace AdventOfCode2019.Models
{
    public class PuzzleContext : DbContext
    {
        public PuzzleContext(DbContextOptions<PuzzleContext> options)
            : base(options)
        {
        }

        public DbSet<Puzzle> Puzzles { get; set; }
    }
}