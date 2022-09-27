namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Kruskal : MazeGenerationAlgorithm
{
    public Kruskal(int size) : base(size)
    {
    }

    public Kruskal(int height, int width) : base(height, width)
    {
    }


    public override Cell[,] Generate(bool generateSteps)
    {
        MazeMap = getUnconnectedMaze(Height, Width);

        return MazeMap;
    }
}