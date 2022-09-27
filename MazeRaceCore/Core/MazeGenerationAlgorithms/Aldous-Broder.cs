namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Aldous_Broder : MazeGenerationAlgorithm
{
    public Aldous_Broder(int size) : base(size)
    {
    }

    public Aldous_Broder(int height, int width) : base(height, width)
    {
    }


    public override Cell[,] Generate(bool withSteps)
    {
        MazeMap = getUnconnectedMaze(Height, Width);

        if (withSteps)
        {
            GenerationSteps = new();
        }

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

                if (withSteps)
                {
                    Tuple<Tuple<int, int>, Tuple<int, int>> t = new Tuple<Tuple<int, int>, Tuple<int, int>>(
                        new Tuple<int, int>(previous.X, previous.Y),
                        new Tuple<int, int>(current.X, current.Y));
                    GenerationSteps.Add(t);
                }
            }
        }

        return MazeMap;
    }
}