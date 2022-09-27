using System.Text;
using MazeRaceCore.Core;
using MazeRaceCore.Core.GameModes;

namespace MazeRaceBlazor.Partials;

public class MazeSvgRenderer
{
    public static string RenderGame(GameMode game)
    {
        var stringBuilder = MazeToSvg(game.CurrentMaze);
        var start = game.currentEndpoints.Item1;
        var end = game.currentEndpoints.Item2;

        stringBuilder.AppendLine(
            $@"{'\t'}<rect class=""start"" fill=""#bbb"" stroke=""#24292e"" stroke-width=""0"" width="".6"" height="".6"" x=""{start.Item2 * 3 + 1.2}"" y=""{start.Item1 * 3 + 1.2}""/>");
        stringBuilder.AppendLine(
            $@"{'\t'}<rect class=""end"" fill=""#323232"" stroke=""#24292e"" stroke-width=""0"" width="".6"" height="".6"" x=""{end.Item2 * 3 + 1.2}"" y=""{end.Item1 * 3 + 1.2}""/>");

        foreach (var racer in game.Racers)
            if (racer.ZCoord == game.Racers[0].ZCoord && racer.Name.Equals("Computer"))
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect class=""player"" fill=""#dc143c"" stroke=""#24292e"" stroke-width=""0"" width="".6"" height="".6"" x=""{racer.YCoord * 3 + 1.2}"" y=""{racer.XCoord * 3 + 1.2}""/>");

            else if (racer.Name.Equals("Player"))
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect class=""player"" fill=""#7abafc"" stroke=""#24292e"" stroke-width=""0"" width="".6"" height="".6"" x=""{racer.YCoord * 3 + 1.2}"" y=""{racer.XCoord * 3 + 1.2}""/>");

        stringBuilder.AppendLine(@"</svg>");
        stringBuilder.AppendLine();
        return stringBuilder.ToString();
    }


    public static string RenderMaze(Cell[,] maze)
    {
        var builder = MazeToSvg(maze);
        builder.AppendLine(@"</svg>");
        builder.AppendLine();
        return builder.ToString();
    }

    private static StringBuilder MazeToSvg(Cell[,] maze)
    {
        var height = maze.GetLength(0);
        var width = maze.GetLength(1);

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(
            $@"<svg width=""60vmin"" height=""60vmin"" viewBox=""0 0 {height * 3} {width * 3}"" preserveAspectRatio=""xMinYMin meet"" xmlns=""http://www.w3.org/2000/svg"" xmlns:svg=""http://www.w3.org/2000/svg"">");
        for (var i = 0; i < height; i++)
        for (var j = 0; j < width; j++)
        {
            // corners
            {
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect fill=""#24292e"" stroke=""#24292e"" stroke-width=""0"" width=""1"" height=""1"" x=""{i * 3 + 0}"" y=""{j * 3 + 0}""/>");
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect fill=""#24292e"" stroke=""#24292e"" stroke-width=""0"" width=""1"" height=""1"" x=""{i * 3 + 2}"" y=""{j * 3 + 0}""/>");
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect fill=""#24292e"" stroke=""#24292e"" stroke-width=""0"" width=""1"" height=""1"" x=""{i * 3 + 0}"" y=""{j * 3 + 2}""/>");
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect fill=""#24292e"" stroke=""#24292e"" stroke-width=""0"" width=""1"" height=""1"" x=""{i * 3 + 2}"" y=""{j * 3 + 2}""/>");
            }

            // sides
            if (maze[j, i].Walls[(int) Walls.Top])
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect fill=""#24292e"" stroke=""#24292e"" stroke-width=""0"" width=""3"" height=""1"" x=""{i * 3 + 0}"" y=""{j * 3 + 0}""/>");
            if (maze[j, i].Walls[(int) Walls.Bottom])
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect fill=""#24292e"" stroke=""#24292e"" stroke-width=""0"" width=""3"" height=""1"" x=""{i * 3 + 0}"" y=""{j * 3 + 2}""/>");
            if (maze[j, i].Walls[(int) Walls.Left])
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect fill=""#24292e"" stroke=""#24292e"" stroke-width=""0"" width=""1"" height=""3"" x=""{i * 3 + 0}"" y=""{j * 3 + 0}""/>");
            if (maze[j, i].Walls[(int) Walls.Right])
                stringBuilder.AppendLine(
                    $@"{'\t'}<rect fill=""#24292e"" stroke=""#24292e"" stroke-width=""0"" width=""1"" height=""3"" x=""{i * 3 + 2}"" y=""{j * 3 + 0}""/>");
        }

        return stringBuilder;
    }
}