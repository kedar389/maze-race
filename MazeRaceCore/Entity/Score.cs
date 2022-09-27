namespace MazeRaceCore.Entity;

[Serializable]
public class Score
{
    public Score(string player, int points, string mode, DateTime playedAt)
    {
        Player = player;
        Points = points;
        Mode = mode;
        PlayedAt = playedAt;
    }


    public int Id { get; set; }

    public string Player { get; set; }

    public int Points { get; set; }

    public string Mode { get; set; }

    public DateTime PlayedAt { get; set; }
}