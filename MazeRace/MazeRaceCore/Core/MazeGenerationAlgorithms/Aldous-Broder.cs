namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Aldous_Broder : MazeGeneratorAlgorithm
{
    public Aldous_Broder(int size) : base(size)
    {
    }

    public Aldous_Broder(int height, int width) : base(height, width)
    {
    }


    public override Cell[,] Generate()
    {
        FillMaze();
        var current = GetRandomCell();
        var remaining = Width * Height;

        List<Cell> allNeighbours = new();

        while (remaining > 0)
        {
            allNeighbours.Clear();
            var unvisitedNeighbours = FindNeighbours(current.X, current.Y, false);
            var visitedNeighbours = FindNeighbours(current.X, current.Y, true);


            allNeighbours.AddRange(unvisitedNeighbours);
            allNeighbours.AddRange(visitedNeighbours);

            var previous = current;
            current = allNeighbours[Random.Next(0, allNeighbours.Count)];

            if (current.VisitedDuringGeneration == false)
            {
                ConnectCells(previous, current);
                remaining--;
            }
        }

        return MazeMap;
    }
}