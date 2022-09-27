using MazeRaceCore.Core.MazeGenerationAlgorithms;

namespace MazeRaceCore.Core;

// TODO mazanie mazov
public class MazeManager
{
    private readonly List<Tuple<Tuple<int, int>, Tuple<int, int>>> endpoints;
    private readonly int size;
    private MazeGenerator generator;

    public MazeManager(int size)
    {
        this.size = size;
        generator = new();
        generator.Size = size;
        endpoints = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();
        Mazes = new List<Cell[,]>();
    }

    private List<Cell[,]> Mazes { get; }


    
    //creates maze and will add it to mazes
    public void CreateMaze()
    {
        var maze = generator.Generate();
        Mazes.Add(maze);
        GenerateEndpoints(maze.GetLength(0), maze.GetLength(1));
    }

    public void CreateMaze(String algo)
    {
        var maze = generator.Generate(algo);
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


    public Cell[,] getMaze(int index)
    {
        if (Mazes.Count > 0) return Mazes[index];

        return MazeGenerationAlgorithm.getUnconnectedMaze(size,size);
    }

    public Tuple<Tuple<int, int>, Tuple<int, int>> getEndpoints(int index)
    {
        if (endpoints.Count > 0) return endpoints[index];

        return null;
    }
}