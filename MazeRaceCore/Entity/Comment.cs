namespace MazeRaceCore.Entity;

[Serializable]
public class Comment
{
    public int Id { get; set; }

    public string Player { get; set; }

    public string? CommentText { get; set; }

    public int NumberOfLikes { get; set; }

    public DateTime PublishedAt { get; set; }
}