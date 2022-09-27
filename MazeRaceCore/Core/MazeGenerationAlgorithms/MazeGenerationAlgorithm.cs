namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

//TODO add option to add seed
public abstract class MazeGenerationAlgorithm
{
    protected Random Random = new();

    protected MazeGenerationAlgorithm(int width, int height)
    {
        Width = width;
        Height = height;
    }

    protected MazeGenerationAlgorithm(int size) : this(size, size)
    {
    }

    public int Height { get; set; }
    public int Width { get; set; }
    protected Cell[,] MazeMap { get; set; }

    
    public List<Tuple<Tuple<int, int>, Tuple<int, int>>> GenerationSteps;


    public abstract Cell[,] Generate(bool withSteps);

    public static void RemoveWalls(Cell a, Cell b)
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


    public static Cell[,] getUnconnectedMaze(int height,int width)
    {
        var unconnectedMaze = new Cell[height, width];
        for (var x = 0; x < height; x++)
        for (var y = 0; y < width; y++)
            unconnectedMaze[x, y] = new Cell(x, y);

        return unconnectedMaze;
    }

    protected void AddStep(Cell cell1, Cell cell2)
    {
        Tuple<Tuple<int, int>, Tuple<int, int>> t = new Tuple<Tuple<int, int>, Tuple<int, int>>(
            new Tuple<int, int>(cell1.X, cell1.Y),
            new Tuple<int, int>(cell2.X, cell2.Y));
        GenerationSteps.Add(t);
    }
}