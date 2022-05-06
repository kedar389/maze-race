namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Kruskal : MazeGeneratorAlgorithm
{
    public Kruskal(int size) : base(size)
    {
    }

    public Kruskal(int height, int width) : base(height, width)
    {
    }


    public override Cell[,] Generate()
    {
        FillMaze();

        return MazeMap;
    }
}