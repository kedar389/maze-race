using MazeRaceCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace MazeRaceCore.Service;

public class MazeRaceDbContext : DbContext
{
    public DbSet<Score> Scores { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Rating> Ratings { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MazeRace;Trusted_Connection=True;");
    }
}