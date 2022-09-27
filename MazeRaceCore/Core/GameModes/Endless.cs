using System.Timers;
using Timer = System.Timers.Timer;

namespace MazeRaceCore.Core.GameModes;

//TODO  refractor time
//TODO refractor interfaces and types of gamemodes

public class Endless : GameMode
{
    private readonly Timer countdown;

    public Endless() : this(12)
    {
    }

    public Endless(int size) : base(size)
    {
        Manager.CreateMaze();
        CurrentMaze = Manager.getMaze(0);
        currentEndpoints = Manager.getEndpoints(0);

        var start = currentEndpoints.Item1;
        Racers?.Add(new Racer("Player", start.Item1, start.Item2, 0));


        Score = 0;
        Counter = 180;
        countdown = new Timer(1000);
        countdown.AutoReset = true;
        countdown.Elapsed += onCountdownElapsed;
        countdown.Start();
    }

    public int Counter { get; set; }

    public int Score { get; set; }

    private void onCountdownElapsed(object? sender, ElapsedEventArgs e)
    {
        Counter--;
        if (Counter < 1)
        {
            countdown.Stop();
            countdown.Dispose();
        }
    }


    public override void UpdateGame(string playerName)
    {
        var player = Racers?.Find(x => x.Name == playerName);
        if (player != null)
        {
            var end = Manager.getEndpoints(player.ZCoord).Item2;

            if (racerAtEnd(player, end))
            {
                Manager.CreateMaze();
                player.ZCoord += 1;
                CurrentMaze = Manager.getMaze(player.ZCoord);
                var start = Manager.getEndpoints(player.ZCoord).Item1;
                player.XCoord = start.Item1;
                player.YCoord = start.Item2;
                currentEndpoints = Manager.getEndpoints(player.ZCoord);

                updateScore();
            }
        }
    }


    public override bool IsFinished()
    {
        if (Counter < 1) return true;

        return false;
    }


    private void updateScore()
    {
        //ked plati podmienka ---------- < 90 =  counter + score * pocet prejdenych mazov
        //viac ako 90 + score * mazov
        if (Racers != null) Score += Counter > 90 ? 90 + 1 * Racers[0].ZCoord + 1 : Counter + 1 * Racers[0].ZCoord + 1;
    }
}