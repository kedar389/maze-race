using System.Timers;
using Timer = System.Timers.Timer;

namespace MazeRaceCore.Core.GameModes;

public class Rush : GameMode
{
    private readonly Timer countdown;

    public Rush() : this(17)
    {
    }

    public Rush(int size) : base(size)
    {
        Manager.CreateMaze();
        CurrentMaze = Manager.getMaze(0);
        currentEndpoints = Manager.getEndpoints(0);
        var start = currentEndpoints.Item1;
        Racers?.Add(new Racer("Player", start.Item1, start.Item2, 0));


        Score = 0;
        Counter = 20;
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
        //TODO update score podla toho ako si blizko
        updateScore();
    }

    public override bool IsFinished()
    {
        if (currentEndpoints.Item2.Item1 == Racers[0].XCoord && currentEndpoints.Item2.Item2 == Racers[0].YCoord ||
            Counter < 1)
            return true;

        return false;
    }


    private void updateScore()
    {
        if (Racers != null) Score += 10;
    }
}