namespace MazeRaceCore.Core;

public class ControlledRacer : Racer
{
    private int _pathIndex;

    public ControlledRacer(string name, int x, int y, int z) : base(name, x, y, z)
    {
        CorrectPath = new List<Tuple<int, int>>();
    }

    public ControlledRacer(string name) : this(name, 0, 0, 0)
    {
    }

    public List<Tuple<int, int>>? CorrectPath { get; }

    internal void Advance()
    {
        if (CorrectPath.Count > 0)
        {
            XCoord = CorrectPath[0].Item1;
            YCoord = CorrectPath[0].Item2;
            CorrectPath.RemoveAt(0);
        }
    }
}