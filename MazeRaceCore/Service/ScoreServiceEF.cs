using MazeRaceCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace MazeRaceCore.Service;

public class ScoreServiceEf : IScoreService
{
    public void AddScore(Score score)
    {
        using var context = new MazeRaceDbContext();
        context.Scores.Add(score);
        context.SaveChanges();
    }


    public IList<Score> GetTopScores(int numberOfScores, int offset, string mode)
    {
        using var context = new MazeRaceDbContext();
        return (from s in context.Scores orderby s.Points descending select s).Where(s => s.Mode.Equals(mode))
            .Skip(offset).Take(numberOfScores)
            .ToList();
    }


    public IList<Score> GetNewestScores(int numberOfScores, int offset, string mode)
    {
        using var context = new MazeRaceDbContext();
        return (from s in context.Scores orderby s.PlayedAt descending select s).Where(s => s.Mode.Equals(mode))
            .Skip(offset).Take(numberOfScores)
            .ToList();
    }


    public void ResetScore()
    {
        using var context = new MazeRaceDbContext();
        context.Database.ExecuteSqlRaw("DELETE FROM Scores");
    }


    public void RemoveScore(string playerName)
    {
        using var context = new MazeRaceDbContext();
        var scoreToRemove = context.Scores.SingleOrDefault(s => s.Player == playerName);


        if (scoreToRemove != null)
        {
            context.Scores.Remove(scoreToRemove);
            context.SaveChanges();
        }
    }


    public void RemoveScore(int scoreId)
    {
        using var context = new MazeRaceDbContext();
        var scoreToRemove = context.Scores.SingleOrDefault(s => s.Id == scoreId);


        if (scoreToRemove != null)
        {
            context.Scores.Remove(scoreToRemove);
            context.SaveChanges();
        }
    }
}