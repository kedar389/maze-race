using System;
using MazeRaceCore.Entity;
using MazeRaceCore.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeRaceTest;

[TestClass]
public class ScoreServiceTestDb
{
    [TestMethod]
    public void AddTestSingle()
    {
        var service = CreateService();

        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});

        Assert.AreEqual(1, service.GetTopScores(10, 0).Count);

        Assert.AreEqual("Jaro", service.GetTopScores(10, 0)[0].Player);
        Assert.AreEqual(100, service.GetTopScores(10, 0)[0].Points);
    }


    [TestMethod]
    public void AddTestMultiple()
    {
        var service = CreateService();

        service.AddScore(new Score {Player = "Zuzka", Points = 20, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Anca", Points = 10, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Peter", Points = 200, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jozo", Points = 50, PlayedAt = DateTime.Now});

        Assert.AreEqual(3, service.GetTopScores(3, 0).Count);

        Assert.AreEqual("Peter", service.GetTopScores(10, 0)[0].Player);
        Assert.AreEqual(200, service.GetTopScores(10, 0)[0].Points);

        Assert.AreEqual("Jaro", service.GetTopScores(10, 0)[1].Player);
        Assert.AreEqual(100, service.GetTopScores(10, 0)[1].Points);

        Assert.AreEqual("Jozo", service.GetTopScores(10, 0)[2].Player);
        Assert.AreEqual(50, service.GetTopScores(10, 0)[2].Points);
    }

    [TestMethod]
    public void ResetTest()
    {
        var service = CreateService();

        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});

        service.ResetScore();
        Assert.AreEqual(0, service.GetTopScores(10, 0).Count);
    }


    [TestMethod]
    public void RemoveTestNameMultiple()
    {
        var service = CreateService();

        service.AddScore(new Score {Player = "Jano", Points = 80, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jozef", Points = 125, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Parok", Points = 458, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Slota", Points = 221, PlayedAt = DateTime.Now});

        service.RemoveScore("Jaro");
        service.RemoveScore("Slota");

        Assert.AreEqual(3, service.GetTopScores(10, 0).Count);
        Assert.AreEqual("Parok", service.GetTopScores(10, 0)[0].Player);
        Assert.AreEqual("Jozef", service.GetTopScores(10, 0)[1].Player);
    }


    [TestMethod]
    public void RemoveTestNameSingle()
    {
        var service = CreateService();
        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});

        service.RemoveScore("Jaro");

        Assert.AreEqual(0, service.GetTopScores(10, 0).Count);
    }

    [TestMethod]
    public void RemoveTestNameNotInFile()
    {
        var service = CreateService();

        service.AddScore(new Score {Player = "Jano", Points = 80, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jozef", Points = 125, PlayedAt = DateTime.Now});

        service.RemoveScore("Matej");

        Assert.AreEqual(3, service.GetTopScores(10, 0).Count);
        Assert.AreEqual("Jozef", service.GetTopScores(10, 0)[0].Player);
        Assert.AreEqual("Jano", service.GetTopScores(10, 0)[2].Player);
    }

    /*
    [TestMethod]
    public void RemoveTestIdMultiple()
    {
        var service = CreateService();

        service.AddScore(new Score {  Player = "Jano", Points = 80, PlayedAt = DateTime.Now });
        service.AddScore(new Score {  Player = "Jaro", Points = 100, PlayedAt = DateTime.Now });
        service.AddScore(new Score {  Player = "Jozef", Points = 125, PlayedAt = DateTime.Now });
        service.AddScore(new Score {  Player = "Parok", Points = 458, PlayedAt = DateTime.Now });
        service.AddScore(new Score {  Player = "Slota", Points = 221, PlayedAt = DateTime.Now });

        service.RemoveScore(2);
        service.RemoveScore(5);

        Assert.AreEqual(3, service.GetTopScores(10, 0).Count);
        Assert.AreEqual("Parok", service.GetTopScores(10, 0)[0].Player);
        Assert.AreEqual("Jozef", service.GetTopScores(10, 0)[1].Player);
    }


    [TestMethod]
    public void RemoveTestIdSingle()
    {
        var service = CreateService();
        service.AddScore(new Score {  Player = "Jaro", Points = 100, PlayedAt = DateTime.Now });

        service.RemoveScore(1);

        Assert.AreEqual(0, service.GetTopScores(10, 0).Count);
    }

    [TestMethod]
    public void RemoveTestIdNotInFile()
    {
        var service = CreateService();

        service.AddScore(new Score {  Player = "Jano", Points = 80, PlayedAt = DateTime.Now });
        service.AddScore(new Score {  Player = "Jaro", Points = 100, PlayedAt = DateTime.Now });
        service.AddScore(new Score {  Player = "Jozef", Points = 125, PlayedAt = DateTime.Now });

        service.RemoveScore(4);

        Assert.AreEqual(3, service.GetTopScores(10, 0).Count);
        Assert.AreEqual("Jozef", service.GetTopScores(10, 0)[0].Player);
        Assert.AreEqual("Jano", service.GetTopScores(10, 0)[2].Player);
    }
    */


    [TestMethod]
    public void GetTopScoresTestWithoutOffset()
    {
        var service = CreateService();

        service.AddScore(new Score {Player = "Jano", Points = 80, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jozef", Points = 125, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Parok", Points = 458, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Slota", Points = 221, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "koro", Points = 45, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "bobo", Points = 21, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Alica", Points = 8787, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Aron", Points = 5522, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Radek", Points = 6544, PlayedAt = DateTime.Now});


        Assert.AreEqual(10, service.GetTopScores(10, 0).Count);
        Assert.AreEqual("Alica", service.GetTopScores(10, 0)[0].Player);
        Assert.AreEqual("Slota", service.GetTopScores(10, 0)[4].Player);
        Assert.AreEqual("Jaro", service.GetTopScores(10, 0)[6].Player);
        Assert.AreEqual("bobo", service.GetTopScores(10, 0)[9].Player);
    }


    [TestMethod]
    public void GetTopScoresTestWithOffset()
    {
        var service = CreateService();

        service.AddScore(new Score {Player = "Jano", Points = 80, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jaro", Points = 100, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Jozef", Points = 125, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Parok", Points = 458, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Slota", Points = 221, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "koro", Points = 45, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "bobo", Points = 21, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Alica", Points = 8787, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Aron", Points = 5522, PlayedAt = DateTime.Now});
        service.AddScore(new Score {Player = "Radek", Points = 6544, PlayedAt = DateTime.Now});


        Assert.AreEqual(5, service.GetTopScores(10, 5).Count);
        Assert.AreEqual("Jozef", service.GetTopScores(10, 5)[0].Player);
        Assert.AreEqual("Jaro", service.GetTopScores(10, 5)[1].Player);
        Assert.AreEqual("Jano", service.GetTopScores(10, 5)[2].Player);
        Assert.AreEqual("koro", service.GetTopScores(10, 5)[3].Player);
    }


    private IScoreService CreateService()
    {
        var service = new ScoreServiceEf();
        service.ResetScore();
        return service;
    }
}