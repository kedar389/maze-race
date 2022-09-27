namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Prim : MazeGenerationAlgorithm
{
    public Prim(int size) : base(size)
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
        current.VisitedDuringGeneration = true;

        var frontier = new HashSet<Cell>();
        frontier.UnionWith(FindNeighbours(current.X, current.Y, false));

        while (frontier.Count > 0)
        {
            var randomNeighbour = Random.Next(0, frontier.Count);
            current = frontier.ElementAt(randomNeighbour);
            frontier.Remove(current);

            var visitedNeighbours = FindNeighbours(current.X, current.Y, true);

            var visitedNeighbour = visitedNeighbours[Random.Next(0, visitedNeighbours.Count)];

            ConnectCells(visitedNeighbour, current);

            if (withSteps)
            {
                AddStep(current,visitedNeighbour);
            }

            var newUnvisitedNeighbours = FindNeighbours(current.X, current.Y, false);

            frontier.UnionWith(newUnvisitedNeighbours);
        }

        return MazeMap;
    }
}