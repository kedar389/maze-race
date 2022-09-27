using MazeRaceCore.Core.SolvingAlgorithms;

namespace MazeRaceCore.Core.GameModes;

public class Custom : GameMode
{
    private readonly int _levelCount;

    private readonly Tuple<int, int> exit;
    private readonly SolverBacktracker solver;

    public Custom(int size, int numOfComputers, int levelCount,String[] algos) : base(size)
    {
        _levelCount = levelCount;

        for (var i = 0; i < levelCount; i++) Manager.CreateMaze(algos[i]);

        CurrentMaze = Manager.getMaze(0);
        currentEndpoints = Manager.getEndpoints(0);
        exit = Manager.getEndpoints(levelCount - 1).Item2;

        solver = new SolverBacktracker(CurrentMaze);

        var start = currentEndpoints.Item1;
        Racers?.Add(new Racer("Player", start.Item1, start.Item2, 0));
        addComputerPlayers(numOfComputers);
        generatePathsAi();
    }


    public override void UpdateGame(string playerName)
    {
        var player = Racers?.Find(x => x.Name == playerName);
        if (player != null)
        {
            var end = Manager.getEndpoints(player.ZCoord).Item2;

            if (racerAtEnd(player, end) && player.ZCoord < _levelCount - 1)
            {
                player.ZCoord += 1;
                CurrentMaze = Manager.getMaze(player.ZCoord);
                var start = Manager.getEndpoints(player.ZCoord).Item1;
                player.XCoord = start.Item1;
                player.YCoord = start.Item2;
                currentEndpoints = Manager.getEndpoints(player.ZCoord);
            }
        }
    }


    public override bool IsFinished()
    {
        if (WhoHasWon() != null) return true;
        return false;
    }


    public Racer? WhoHasWon()
    {
        if (Racers != null)
            foreach (var racer in Racers)
                if (racerAtEnd(racer, exit))
                    return racer;

        return null;
    }

    private void generatePathsAi()
    {
        foreach (var racer in Racers)
            if (racer.GetType() == typeof(ControlledRacer))
            {
                var controlled = (ControlledRacer) racer;
                solver.FindPath(currentEndpoints.Item1, currentEndpoints.Item2, controlled.CorrectPath);
            }
    }


    public void AdvanceAi()
    {
        if (Racers != null)

            foreach (var racer in Racers)
                if (racer.GetType() == typeof(ControlledRacer))
                {
                    var controlled = (ControlledRacer) racer;
                    controlled.Advance();


                    var end = Manager.getEndpoints(controlled.ZCoord).Item2;

                    if (racerAtEnd(controlled, end) && racer.ZCoord < _levelCount - 1)
                    {
                        racer.ZCoord += 1;
                        var mazeStart = Manager.getEndpoints(racer.ZCoord).Item1;
                        var mazeEnd = Manager.getEndpoints(racer.ZCoord).Item2;

                        solver.Maze = Manager.getMaze(racer.ZCoord);
                        solver.FindPath(mazeStart, mazeEnd, controlled.CorrectPath);
                        racer.XCoord = mazeStart.Item1;
                        racer.YCoord = mazeStart.Item2;
                    }
                }
    }


    private void addComputerPlayers(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var racer = new ControlledRacer("Computer");
            racer.XCoord = currentEndpoints.Item1.Item1;
            racer.YCoord = currentEndpoints.Item1.Item2;
            Racers.Add(racer);
        }
    }
}