namespace MazeRaceCore.Core;

public enum Walls
{
    Left,
    Bottom,
    Right,
    Top
}

public class Cell
{
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        VisitedDuringGeneration = false;
        Walls = new[] {true, true, true, true};
    }

    public int X { get; }
    public int Y { get; }

    public bool VisitedDuringGeneration { get; set; }
    public bool[] Walls { get; set; }
}