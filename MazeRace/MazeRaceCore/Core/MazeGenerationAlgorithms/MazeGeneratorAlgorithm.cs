namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

//TODO add option to add seed
public abstract class MazeGeneratorAlgorithm
{
    protected Random Random = new();

    protected MazeGeneratorAlgorithm(int width, int height)
    {
        Width = width;
        Height = height;
    }

    protected MazeGeneratorAlgorithm(int size) : this(size, size)
    {
    }

    public int Height { get; set; }
    public int Width { get; set; }
    protected Cell[,] MazeMap { get; set; }


    public abstract Cell[,] Generate();

    protected void FillMaze()
    {
        MazeMap = new Cell[Width, Height];

        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
            MazeMap[x, y] = new Cell(x, y);
    }


    protected static void RemoveWalls(Cell a, Cell b)
    {
        var x = a.X - b.X;

        if (x == 1)
        {
            a.Walls[(int) Walls.Top] = false;
            b.Walls[(int) Walls.Bottom] = false;
        }

        else if (x == -1)
        {
            a.Walls[(int) Walls.Bottom] = false;
            b.Walls[(int) Walls.Top] = false;
        }

        var y = a.Y - b.Y;

        if (y == 1)
        {
            a.Walls[(int) Walls.Left] = false;
            b.Walls[(int) Walls.Right] = false;
        }
        else if (y == -1)
        {
            a.Walls[(int) Walls.Right] = false;
            b.Walls[(int) Walls.Left] = false;
        }
    }

    //Finds neighbours of a tile based on x and y,
    //visited specifies if neighbours we are looking for
    //have been visited during a generation or not.
    //returns neighbours or an empty list.
    protected List<Cell> FindNeighbours(int x, int y, bool visited)
    {
        var neighbours = new List<Cell>();
        if (x > 0 && MazeMap[x - 1, y].VisitedDuringGeneration == visited) neighbours.Add(MazeMap[x - 1, y]);

        if (x < Height - 1 && MazeMap[x + 1, y].VisitedDuringGeneration == visited) neighbours.Add(MazeMap[x + 1, y]);

        if (y > 0 && MazeMap[x, y - 1].VisitedDuringGeneration == visited) neighbours.Add(MazeMap[x, y - 1]);

        if (y < Width - 1 && MazeMap[x, y + 1].VisitedDuringGeneration == visited) neighbours.Add(MazeMap[x, y + 1]);

        return neighbours;
    }

    protected Cell GetRandomCell()
    {
        return MazeMap[Random.Next(0, Height), Random.Next(0, Width)];
    }

    protected void ConnectCells(Cell from, Cell to)
    {
        RemoveWalls(from, to);
        to.VisitedDuringGeneration = true;
    }
}