﻿namespace MazeRaceCore.Core.MazeGenerationAlgorithms;

public class Backtracker : MazeGenerationAlgorithm
{
    public Backtracker(int size) : base(size)
    {
    }

    public Backtracker(int height, int width) : base(height, width)
    {
    }


    public override Cell[,] Generate(bool withSteps)
    {
        MazeMap = getUnconnectedMaze(Height, Width);

        if (withSteps)
        {
            GenerationSteps = new();
        }

        var cellStack = new Stack<Cell>();
        var current = MazeMap[0, 0];

        current.VisitedDuringGeneration = true;
        cellStack.Push(current);

        while (cellStack.Count > 0)
        {
            current = cellStack.Peek();
            var neighbours = FindNeighbours(current.X, current.Y, false);
            if (neighbours.Count > 0)
            {
                var next = neighbours[Random.Next(0, neighbours.Count)];
                cellStack.Push(next);
                ConnectCells(current, next);

                if (withSteps)
                {
                    AddStep(current,next);
                }

                continue;
            }

            cellStack.Pop();
        }

        return MazeMap;
    }
}