namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Sidewinder : MazeGeneratorAlgorithm
{
    public Sidewinder(int size) : base(size)
    {
    }

    public Sidewinder(int height, int width) : base(height, width)
    {
    }


    public override Cell[,] Generate()
    {
        FillMaze();

        for (var x = 0; x < Height; x++)
        {
            var runStart = 0;
            for (var y = 0; y < Width; y++)
                if (x > 0 && (y + 1 == Width || Random.Next(0, 2) == 0))
                {
                    var upperCell = runStart + Random.Next(0, y - runStart + 1);

                    ConnectCells(MazeMap[x, upperCell], MazeMap[x - 1, upperCell]);
                    runStart = y + 1;
                }

                else if (y + 1 < Width)
                {
                    ConnectCells(MazeMap[x, y], MazeMap[x, y + 1]);
                }
        }

        return MazeMap;
    }
}