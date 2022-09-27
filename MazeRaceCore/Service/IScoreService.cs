using MazeRaceCore.Entity;

namespace MazeRaceCore.Service;

public interface IScoreService
{
    void AddScore(Score score);

    IList<Score> GetTopScores(int numberOfScores, int offset, string mode);

    IList<Score> GetNewestScores(int numberOfScores, int offset, string mode);

    void ResetScore();

    void RemoveScore(string playerName);

    void RemoveScore(int scoreId);
}