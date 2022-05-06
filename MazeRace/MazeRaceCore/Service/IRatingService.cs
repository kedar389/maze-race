using MazeRaceCore.Entity;

namespace MazeRaceCore.Service;

public interface IRatingService
{
    void AddRating(Rating rating);

    int GetAverageRating();

    IList<Rating> GetAllRatings();

    void ResetRatings();

    void RemoveRating(string userName);

    void RemoveRating(int ratingId);
}