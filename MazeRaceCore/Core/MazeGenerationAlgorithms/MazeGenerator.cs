namespace MazeRaceCore.Core.MazeGenerationAlgorithms;
public class MazeGenerator
{

    public int Size {get; set; }
    private static readonly Random Random = new();
    private readonly Dictionary<String, MazeGenerationAlgorithm> _algorithms = new();


    //TODO setting of size,other algos
    public MazeGenerator()
    {

        Size = 10;
        _algorithms.Add("aldousbroder", new Aldous_Broder(Size));
        _algorithms.Add("backtracker", new Backtracker(Size));
        _algorithms.Add("binarytree", new BinaryTree(Size));
        _algorithms.Add("growingtree", new GrowingTree(Size));
        _algorithms.Add("prim", new Prim(Size));
        _algorithms.Add("sidewinder", new Sidewinder(Size));
        // algorithms.Add(Algorithm.Kruskal, new Kruskal(size));
        //algorithms.Add(Algorithm.Eller, new Eller(size));
    }


    //returns maze where algorithm for creation was picked random
    public Cell[,] Generate()
    {
        String[] algoValues = { "aldousbroder", "backtracker", "binarytree", "growingtree", "prim", "sidewinder" };
        var randomValue = algoValues[Random.Next(algoValues.Length)];
        return Generate(randomValue);
    }

    //returns maze created by specified algorithm
    public Cell[,] Generate(String algo)
    {
        var maze = _algorithms[algo].Generate(false);
        return maze;
    }


    public List<Tuple<Tuple<int, int>, Tuple<int, int>>> GenerateSteps(String algo)
    {
         _algorithms[algo].Generate(true);
        return _algorithms[algo].GenerationSteps;
    }
}