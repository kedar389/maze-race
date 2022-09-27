namespace MazeRaceCore.Core;

public class Racer
{
    public Racer(string name, int x, int y, int z)
    {
        Name = name;

        XCoord = x;
        YCoord = y;
        ZCoord = z;
    }


    public Racer(string name)
    {
        Name = name;

        XCoord = 0;
        YCoord = 0;
        ZCoord = 0;
    }


    public int XCoord { get; set; }
    public int YCoord { get; set; }
    public int ZCoord { get; set; }
    public string Name { get; set; }


    internal bool Move(Walls move)
    {
        switch (move)
        {
            case Walls.Left:
                YCoord = YCoord - 1;
                return true;

            case Walls.Right:
                YCoord = YCoord + 1;
                return true;

            case Walls.Top:
                XCoord = XCoord - 1;
                return true;

            case Walls.Bottom:
                XCoord = XCoord + 1;
                return true;
            default: return false;
        }
    }
}