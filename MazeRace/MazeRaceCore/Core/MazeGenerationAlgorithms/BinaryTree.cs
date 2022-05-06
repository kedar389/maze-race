namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class BinaryTree : MazeGeneratorAlgorithm
{
    public BinaryTree(int size) : base(size)
    {
    }

    public BinaryTree(int height, int width) : base(height, width)
    {
    }


    //Binary tree is often skewed to some direction , "NW","NE":"SW":"SE":
    //This one is skewed to SE,can be modified to be skewed to other directions
    public override Cell[,] Generate()
    {
        FillMaze();

        for (var x = 0; x < Height; x++)
        for (var y = 0; y < Width; y++)
        {
            var cells = new List<Cell>();

            if (x + 1 < Height)
                cells.Add(MazeMap[x + 1, y]);

            if (y + 1 < Width)
                cells.Add(MazeMap[x, y + 1]);

            if (cells.Count > 0)
            {
                var cell = cells[Random.Next(0, cells.Count)];
                ConnectCells(MazeMap[x, y], cell);
            }
        }

        return MazeMap;
    }
}