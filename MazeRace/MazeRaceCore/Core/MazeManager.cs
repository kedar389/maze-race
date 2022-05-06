using MazeRaceCore.Core.MazeGenerationAlgorithms;

namespace MazeRaceCore.Core;

// TODO mazanie mazov,zmaz funkcie ktore nepouzivas.
public class MazeManager
{
    private static readonly Random Random = new();
    private readonly Dictionary<String, MazeGeneratorAlgorithm> _algorithms = new();
    private readonly List<Tuple<Tuple<int, int>, Tuple<int, int>>> endpoints;
    private readonly int size;

    public MazeManager(int size)
    {
        this.size = size;
        _algorithms.Add("aldousbroder", new Aldous_Broder(size));
        _algorithms.Add("backtracker", new Backtracker(size));
        _algorithms.Add("binarytree", new BinaryTree(size));
        _algorithms.Add("growingtree", new GrowingTree(size));
        _algorithms.Add("prim", new Prim(size));
        _algorithms.Add("sidewinder", new Sidewinder(size));

        //algorithms.Add(Algorithm.Kruskal, new Kruskal(size));
        //algorithms.Add(Algorithm.Eller, new Eller(size));


        endpoints = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();
        Mazes = new List<Cell[,]>();
    }

    private List<Cell[,]> Mazes { get; }


    //returns maze where algorithm for creation was picked random
    public Cell[,] Generate()
    {
        String[] algoValues = {"aldousbroder", "backtracker", "binarytree", "growingtree", "prim", "sidewinder"};
        var randomValue = algoValues[Random.Next(algoValues.Length)];
        return Generate(randomValue);
    }

    //returns maze created by specified algorithm
    public Cell[,] Generate(String algo)
    {
        var maze = _algorithms[algo].Generate();
        return maze;
    }

    //creates maze and will add it to mazes
    public void CreateMaze()
    {
        var maze = Generate();
        Mazes.Add(maze);
        GenerateEndpoints(maze.GetLength(0), maze.GetLength(1));
    }

    public void CreateMaze(String algo)
    {
        var maze = Generate(algo);
        Mazes.Add(maze);
        GenerateEndpoints(maze.GetLength(0), maze.GetLength(1));
    }


    private void GenerateEndpoints(int width, int height)
    {
        var start = new Tuple<int, int>(new Random().Next(width / 2 - 1),
            new Random().Next(height / 2 - 1));

        var end = new Tuple<int, int>(new Random().Next(width / 2, width),
            new Random().Next(height / 2, height));

        endpoints.Add(new Tuple<Tuple<int, int>, Tuple<int, int>>(start, end));
    }


    public static Cell[,] getUnconnectedMaze(int size)
    {
        var unconnectedMaze = new Cell[size, size];
        for (var x = 0; x < size; x++)
        for (var y = 0; y < size; y++)
            unconnectedMaze[x, y] = new Cell(x, y);

        return unconnectedMaze;
    }

    public Cell[,] getMaze(int index)
    {
        if (Mazes.Count > 0) return Mazes[index];

        return getUnconnectedMaze(size);
    }

    public Tuple<Tuple<int, int>, Tuple<int, int>> getEndpoints(int index)
    {
        if (endpoints.Count > 0) return endpoints[index];

        return null;
    }
}