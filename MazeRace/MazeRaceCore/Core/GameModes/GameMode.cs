namespace MazeRaceCore.Core.GameModes;

public abstract class GameMode
{
    public Tuple<Tuple<int, int>, Tuple<int, int>> currentEndpoints = null;
    protected MazeManager Manager;

    protected GameMode(int size)
    {
        Manager = new MazeManager(size);
        Racers = new List<Racer>();
    }

    public List<Racer>? Racers { get; }

    public Cell[,] CurrentMaze { get; set; }


    public abstract void UpdateGame(string playerName);

    public abstract bool IsFinished();

    public bool MovePlayer(Walls move, string playerName)
    {
        var player = Racers?.Find(x => x.Name == playerName);

        if (player != null)
        {
            var currentCell = CurrentMaze[player.XCoord, player.YCoord];

            if (!currentCell.Walls[(int) move])
            {
                player.Move(move);

                return true;
            }
        }

        return false;
    }


    protected bool racerAtEnd(Racer racer, Tuple<int, int> end)
    {
        if (racer.XCoord == end.Item1 && racer.YCoord == end.Item2) return true;

        return false;
    }
}