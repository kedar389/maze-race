namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Eller : MazeGeneratorAlgorithm
{
    public Eller(int size) : base(size)
    {
    }

    public Eller(int height, int width) : base(height, width)
    {
    }


    public override Cell[,] Generate()
    {
        FillMaze();

        return MazeMap;
    }
}