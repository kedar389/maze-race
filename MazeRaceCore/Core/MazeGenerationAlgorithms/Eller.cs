namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Eller : MazeGenerationAlgorithm
{
    public Eller(int size) : base(size)
    {
    }

    public Eller(int height, int width) : base(height, width)
    {
    }


    public override Cell[,] Generate(bool generateSteps)
    {
        MazeMap = getUnconnectedMaze(Height, Width);

        return MazeMap;
    }
}