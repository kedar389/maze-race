namespace MazeRaceCore.Core.SolvingAlgorithms;

public class SolverBacktracker
{
    public SolverBacktracker(Cell[,] maze)
    {
        Maze = maze;
    }

    public Cell[,] Maze { get; set; }


    public bool FindPath(Tuple<int, int> start, Tuple<int, int> end, List<Tuple<int, int>>? correctPath)
    {
        var size = Maze.GetLength(0);
        var wasHere = new bool[size, size];

        for (var x = 0; x < size; x++)
        for (var y = 0; y < size; y++)
            wasHere[x, y] = false;

        return RecursiveFind(start, end, wasHere, correctPath);
    }


    //Recursively tries to find path that leads to end,
    //if it  has found tile that was already visited, it will backtrack and tries different direction.
    private bool RecursiveFind(Tuple<int, int> start, Tuple<int, int> end, bool[,] wasHere,
        List<Tuple<int, int>>? correctPath)
    {
        var size = Maze.GetLength(0);


        if (start.Item1 == end.Item1 && start.Item2 == end.Item2)
        {
            correctPath?.Insert(0, start);
            return true;
        }

        if (wasHere[start.Item1, start.Item2]) return false;

        wasHere[start.Item1, start.Item2] = true;

        //This IF tries all possible directions that we can try from our current position in maze, 
        //Conditions : to not be at map boundary,  there is no wall, we found end.

        if (start.Item2 != 0 && !Maze[start.Item1, start.Item2].Walls[(int) Walls.Left] &&
            RecursiveFind(new Tuple<int, int>(start.Item1, start.Item2 - 1), end, wasHere, correctPath) ||
            start.Item2 != size - 1 && !Maze[start.Item1, start.Item2].Walls[(int) Walls.Right] &&
            RecursiveFind(new Tuple<int, int>(start.Item1, start.Item2 + 1), end, wasHere, correctPath) ||
            start.Item1 != 0 && !Maze[start.Item1, start.Item2].Walls[(int) Walls.Top] &&
            RecursiveFind(new Tuple<int, int>(start.Item1 - 1, start.Item2), end, wasHere, correctPath) ||
            start.Item1 != size - 1 && !Maze[start.Item1, start.Item2].Walls[(int) Walls.Bottom] &&
            RecursiveFind(new Tuple<int, int>(start.Item1 + 1, start.Item2), end, wasHere, correctPath)
           )
        {
            correctPath?.Insert(0, start);
            return true;
        }

        return false;
    }
}