namespace MazeRaceCore.Entity;

[Serializable]
public class Rating
{
    public int Id { get; set; }

    public string Player { get; set; }

    public int NumberOfPoints { get; set; }
}