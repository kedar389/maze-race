namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class GrowingTree : MazeGeneratorAlgorithm
{
    public GrowingTree(int size) : base(size)
    {
    }

    public GrowingTree(int height, int width) : base(height, width)
    {
    }


    //This algoritm is similar either to backtracking or Prim,
    // due to this attribute we will switch between those two
    //at 50/50 chance.
    public override Cell[,] Generate()
    {
        FillMaze();

        var cells = new List<Cell> {GetRandomCell()};
        cells[0].VisitedDuringGeneration = true;


        while (cells.Count > 0)
        {
            //here do pick a cell ,first ,last ,random, idk
            var current = PickCell(cells);


            var unvisitedNeighbours = FindNeighbours(current.X, current.Y, false);

            //if picked cell does not have neighbours, remove from cells, 
            if (unvisitedNeighbours.Count == 0)
            {
                cells.Remove(current);
                continue;
            }

            //connect to random neighbour, add it to cells
            var connectTo = unvisitedNeighbours[Random.Next(0, unvisitedNeighbours.Count)];
            ConnectCells(current, connectTo);
            cells.Add(connectTo);
        }


        return MazeMap;
    }

    //choice == 0, will behave like Prims algorithm
    //choice == 1; will behave like backtracker,
    //can be modified to have different ratios on behaviours.
    private Cell PickCell(List<Cell> cells)
    {
        var choice = Random.Next(0, 2);

        switch (choice)
        {
            case 0:
                return cells[0];
            default:
                return cells[Random.Next(0, cells.Count)];
        }
    }
}