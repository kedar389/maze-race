using MazeRaceCore.Entity;
using MazeRaceCore.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeRaceTest;

[TestClass]
public class RatingServiceTestDb
{
    [TestMethod]
    public void GetAverageRatingsTestMultipleEntries()
    {
        var service = CreateService();

        Assert.AreEqual(0, service.GetAllRatings().Count);

        service.AddRating(new Rating {Player = "Jaro", NumberOfPoints = 100});
        service.AddRating(new Rating {Player = "bob", NumberOfPoints = 225});
        service.AddRating(new Rating {Player = "Dio", NumberOfPoints = 41});
        service.AddRating(new Rating {Player = "Jojo", NumberOfPoints = 25});

        Assert.AreEqual(4, service.GetAllRatings().Count);
        Assert.AreEqual(97, service.GetAverageRating());
    }


    [TestMethod]
    public void GetAverageRatingsTestOneEntry()
    {
        var service = CreateService();

        Assert.AreEqual(0, service.GetAllRatings().Count);

        service.AddRating(new Rating {Player = "Jaro", NumberOfPoints = 100});

        Assert.AreEqual(1, service.GetAllRatings().Count);
        Assert.AreEqual(100, service.GetAverageRating());
    }

    [TestMethod]
    public void GetAverageRatingsTestZeroEntry()
    {
        var service = CreateService();

        Assert.AreEqual(0, service.GetAllRatings().Count);
        Assert.AreEqual(0, service.GetAverageRating());
    }


    [TestMethod]
    public void AddTest()
    {
        var service = CreateService();
        service.AddRating(new Rating {Player = "bob", NumberOfPoints = 225});
        service.AddRating(new Rating {Player = "Jaro", NumberOfPoints = 100});

        Assert.AreEqual(2, service.GetAllRatings().Count);

        Assert.AreEqual("bob", service.GetAllRatings()[0].Player);
        Assert.AreEqual(225, service.GetAllRatings()[0].NumberOfPoints);

        Assert.AreEqual("Jaro", service.GetAllRatings()[1].Player);
        Assert.AreEqual(100, service.GetAllRatings()[1].NumberOfPoints);
    }


    [TestMethod]
    public void ResetTest()
    {
        var service = CreateService();

        service.AddRating(new Rating {Player = "Jaro", NumberOfPoints = 100});
        service.AddRating(new Rating {Player = "bob", NumberOfPoints = 225});

        service.ResetRatings();
        Assert.AreEqual(0, service.GetAllRatings().Count);
    }


    [TestMethod]
    public void RemoveTestByNameMultiple()
    {
        var service = CreateService();

        service.AddRating(new Rating {Player = "Jaro", NumberOfPoints = 100});
        service.AddRating(new Rating {Player = "bob", NumberOfPoints = 225});
        service.AddRating(new Rating {Player = "Dio", NumberOfPoints = 41});
        service.AddRating(new Rating {Player = "Jojo", NumberOfPoints = 25});

        service.RemoveRating("Jaro");
        service.RemoveRating("Dio");

        Assert.AreEqual(2, service.GetAllRatings().Count);
        Assert.AreEqual("bob", service.GetAllRatings()[0].Player);
        Assert.AreEqual("Jojo", service.GetAllRatings()[1].Player);
    }


    [TestMethod]
    public void RemoveTestByNameOneLeft()
    {
        var service = CreateService();
        service.AddRating(new Rating {Player = "Dio", NumberOfPoints = 41});

        service.RemoveRating("Dio");

        Assert.AreEqual(0, service.GetAllRatings().Count);
    }

    [TestMethod]
    public void RemoveTestByNameNotInFile()
    {
        var service = CreateService();

        service.AddRating(new Rating {Player = "Jaro", NumberOfPoints = 100});
        service.AddRating(new Rating {Player = "bob", NumberOfPoints = 225});
        service.AddRating(new Rating {Player = "Dio", NumberOfPoints = 41});


        service.RemoveRating("Matej");

        Assert.AreEqual(3, service.GetAllRatings().Count);
        Assert.AreEqual("Jaro", service.GetAllRatings()[0].Player);
        Assert.AreEqual("Dio", service.GetAllRatings()[2].Player);
    }


    /*
    [TestMethod]
    public void RemoveTestByIdMultiple()
    {
        var service = CreateService();

        service.AddRating(new Rating {  Player = "Jaro", NumberOfPoints = 100 });
        service.AddRating(new Rating {  Player = "bob", NumberOfPoints = 225 });
        service.AddRating(new Rating {  Player = "Dio", NumberOfPoints = 41 });
        service.AddRating(new Rating {  Player = "Jojo", NumberOfPoints = 25 });

        service.RemoveRating(1);
        service.RemoveRating(3);

        Assert.AreEqual(2, service.GetAllRatings().Count);
        Assert.AreEqual("bob", service.GetAllRatings()[0].Player);
        Assert.AreEqual("Jojo", service.GetAllRatings()[1].Player);
    }


    [TestMethod]
    public void RemoveTestByIdOneLeft()
    {
        var service = CreateService();
        service.AddRating(new Rating { Id = 3, Player = "Dio", NumberOfPoints = 41 });

        service.RemoveRating(3);

        Assert.AreEqual(0, service.GetAllRatings().Count);
    }

    [TestMethod]
    public void RemoveTestByIdNotInFile()
    {
        var service = CreateService();

        service.AddRating(new Rating { Id = 1, Player = "Jaro", NumberOfPoints = 100 });
        service.AddRating(new Rating { Id = 2, Player = "bob", NumberOfPoints = 225 });
        service.AddRating(new Rating { Id = 3, Player = "Dio", NumberOfPoints = 41 });

        service.RemoveRating(4);

        Assert.AreEqual(3, service.GetAllRatings().Count);
        Assert.AreEqual("Jaro", service.GetAllRatings()[0].Player);
        Assert.AreEqual("Dio", service.GetAllRatings()[2].Player);
    }
    */


    private IRatingService CreateService()
    {
        var service = new RatingServiceEf();
        service.ResetRatings();
        return service;
    }
}