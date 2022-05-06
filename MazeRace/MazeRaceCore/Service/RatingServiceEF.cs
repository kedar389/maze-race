using MazeRaceCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace MazeRaceCore.Service;

public class RatingServiceEf : IRatingService
{
    public void AddRating(Rating rating)
    {
        using var context = new MazeRaceDbContext();
        context.Ratings.Add(rating);
        context.SaveChanges();
    }


    public int GetAverageRating()
    {
        using var context = new MazeRaceDbContext();
        if (context.Ratings.Any()) return (int) context.Ratings.Average(s => s.NumberOfPoints);

        return 0;
    }

    public IList<Rating> GetAllRatings()
    {
        using var context = new MazeRaceDbContext();
        return (from s in context.Ratings select s).ToList();
    }


    public void ResetRatings()
    {
        using var context = new MazeRaceDbContext();
        context.Database.ExecuteSqlRaw("DELETE FROM Ratings");
    }

    public void RemoveRating(string userName)
    {
        using var context = new MazeRaceDbContext();
        var ratingToRemove = context.Ratings.SingleOrDefault(s => s.Player == userName);


        if (ratingToRemove != null)
        {
            context.Ratings.Remove(ratingToRemove);
            context.SaveChanges();
        }
    }

    public void RemoveRating(int ratingId)
    {
        using var context = new MazeRaceDbContext();
        var ratingToRemove = context.Ratings.SingleOrDefault(s => s.Id == ratingId);


        if (ratingToRemove != null)
        {
            context.Ratings.Remove(ratingToRemove);
            context.SaveChanges();
        }
    }
}