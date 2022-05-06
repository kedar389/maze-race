using System;
using MazeRaceCore.Entity;
using MazeRaceCore.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeRaceTest;

[TestClass]
public class CommentServiceTestDb
{
    [TestMethod]
    public void AddTest()
    {
        var service = CreateService();
        service.AddComment(new Comment
        {
            Player = "Jaro",
            CommentText = "Something",
            NumberOfLikes = 100,
            PublishedAt = new DateTime(2015, 12, 31)
        });

        service.AddComment(new Comment
            {Player = "bob", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = new DateTime(2016, 12, 31)});

        Assert.AreEqual(2, service.GetTopComments(10, 0).Count);

        Assert.AreEqual("bob", service.GetTopComments(10, 0)[1].Player);
        Assert.AreEqual(0, DateTime.Compare(service.GetTopComments(10, 0)[1].PublishedAt, new DateTime(2016, 12, 31)));
        Assert.AreEqual(-225, service.GetTopComments(10, 0)[1].NumberOfLikes);


        Assert.AreEqual("Jaro", service.GetTopComments(10, 0)[0].Player);
        Assert.AreEqual(0, DateTime.Compare(service.GetTopComments(10, 0)[0].PublishedAt, new DateTime(2015, 12, 31)));
        Assert.AreEqual(100, service.GetTopComments(10, 0)[0].NumberOfLikes);
    }

    [TestMethod]
    public void ResetTest()
    {
        var service = CreateService();

        service.AddComment(new Comment
            {Player = "Jaro", CommentText = "Something", NumberOfLikes = 100, PublishedAt = DateTime.Now});
        service.AddComment(new Comment
            {Player = "bob", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = DateTime.Now});

        service.ResetComments();
        Assert.AreEqual(0, service.GetTopComments(10, 0).Count);
    }


    [TestMethod]
    public void GetTopCommentsTest()
    {
        var service = CreateService();

        Assert.AreEqual(0, service.GetNewestComments(10, 0).Count);

        service.AddComment(new Comment {Player = "Jaro", NumberOfLikes = 100, PublishedAt = DateTime.Now});
        service.AddComment(new Comment {Player = "bob", NumberOfLikes = -225, PublishedAt = DateTime.Now});
        service.AddComment(new Comment {Player = "Dio", NumberOfLikes = -41, PublishedAt = DateTime.Now});
        service.AddComment(new Comment {Player = "Jojo", NumberOfLikes = 25, PublishedAt = DateTime.Now});

        Assert.AreEqual(4, service.GetTopComments(10, 0).Count);

        Assert.AreEqual("Jaro", service.GetTopComments(10, 0)[0].Player);
        Assert.AreEqual(100, service.GetTopComments(10, 0)[0].NumberOfLikes);

        Assert.AreEqual("Jojo", service.GetTopComments(10, 0)[1].Player);
        Assert.AreEqual(25, service.GetTopComments(10, 0)[1].NumberOfLikes);

        Assert.AreEqual("Dio", service.GetTopComments(10, 0)[2].Player);
        Assert.AreEqual(-41, service.GetTopComments(10, 0)[2].NumberOfLikes);

        Assert.AreEqual("bob", service.GetTopComments(10, 0)[3].Player);
        Assert.AreEqual(-225, service.GetTopComments(10, 0)[3].NumberOfLikes);
    }


    [TestMethod]
    public void GetNewestCommentsTest()
    {
        var service = CreateService();

        Assert.AreEqual(0, service.GetNewestComments(10, 0).Count);

        service.AddComment(new Comment
        {
            Player = "Jaro",
            CommentText = "Something",
            NumberOfLikes = 100,
            PublishedAt = new DateTime(2019, 12, 31)
        });
        service.AddComment(new Comment
            {Player = "bob", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = new DateTime(2018, 12, 31)});
        service.AddComment(new Comment
            {Player = "Dio", CommentText = "Good", NumberOfLikes = -41, PublishedAt = new DateTime(2016, 12, 31)});
        service.AddComment(new Comment
            {Player = "Jojo", CommentText = "LOL", NumberOfLikes = 25, PublishedAt = new DateTime(2015, 12, 31)});

        Assert.AreEqual(4, service.GetTopComments(10, 0).Count);

        Assert.AreEqual("Jaro", service.GetNewestComments(10, 0)[0].Player);
        Assert.AreEqual(0,
            DateTime.Compare(service.GetNewestComments(10, 0)[0].PublishedAt, new DateTime(2019, 12, 31)));

        Assert.AreEqual("bob", service.GetNewestComments(10, 0)[1].Player);
        Assert.AreEqual(0,
            DateTime.Compare(service.GetNewestComments(10, 0)[1].PublishedAt, new DateTime(2018, 12, 31)));

        Assert.AreEqual("Dio", service.GetNewestComments(10, 0)[2].Player);
        Assert.AreEqual(0,
            DateTime.Compare(service.GetNewestComments(10, 0)[2].PublishedAt, new DateTime(2016, 12, 31)));

        Assert.AreEqual("Jojo", service.GetNewestComments(10, 0)[3].Player);
        Assert.AreEqual(0,
            DateTime.Compare(service.GetNewestComments(10, 0)[3].PublishedAt, new DateTime(2015, 12, 31)));
    }


    /*
    [TestMethod]
    public void LikeCommentTest()
    {
        var service = CreateService();

        Assert.AreEqual(0, service.GetNewestComments(10, 0).Count);

        service.AddComment(new Comment
        {
            Id = 1,
            Player = "Jaro",
            CommentText = "Something",
            NumberOfLikes = 100,
            PublishedAt = new DateTime(2019, 12, 31)
        });

        Assert.AreEqual(1, service.GetTopComments(10, 0).Count);

        service.LikeComment(1);

        Assert.AreEqual(101, service.GetNewestComments(10, 0)[0].NumberOfLikes);
    }

    [TestMethod]
    public void DislikeCommentTest()
    {
        var service = CreateService();

        Assert.AreEqual(0, service.GetNewestComments(10, 0).Count);

        service.AddComment(new Comment
        {
            Id = 1,
            Player = "Jaro",
            CommentText = "Something",
            NumberOfLikes = 100,
            PublishedAt = new DateTime(2019, 12, 31)
        });

        Assert.AreEqual(1, service.GetTopComments(10, 0).Count);

        service.DislikeComment(1);

        Assert.AreEqual(99, service.GetNewestComments(10, 0)[0].NumberOfLikes);
    }
    */


    [TestMethod]
    public void RemoveTestByNameMultiple()
    {
        var service = CreateService();

        service.AddComment(new Comment
            {Player = "Jaro", CommentText = "Something", NumberOfLikes = 100, PublishedAt = DateTime.Now});
        service.AddComment(new Comment
            {Player = "bob", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = DateTime.Now});
        service.AddComment(new Comment
            {Player = "Dio", CommentText = "Good", NumberOfLikes = -41, PublishedAt = DateTime.Now});
        service.AddComment(new Comment
            {Player = "Jojo", CommentText = "LOL", NumberOfLikes = 25, PublishedAt = DateTime.Now});

        service.RemoveComment("Jaro");
        service.RemoveComment("Dio");

        Assert.AreEqual(2, service.GetTopComments(10, 0).Count);
        Assert.AreEqual("Jojo", service.GetTopComments(10, 0)[0].Player);
        Assert.AreEqual("bob", service.GetTopComments(10, 0)[1].Player);
    }


    [TestMethod]
    public void RemoveTestByNameSingle()
    {
        var service = CreateService();
        service.AddComment(new Comment
            {Player = "Dio", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = DateTime.Now});

        service.RemoveComment("Dio");

        Assert.AreEqual(0, service.GetTopComments(10, 0).Count);
    }

    [TestMethod]
    public void RemoveTestByNameNotInFile()
    {
        var service = CreateService();

        service.AddComment(new Comment
            {Player = "bob", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = DateTime.Now});
        service.AddComment(new Comment
            {Player = "Dio", CommentText = "Good", NumberOfLikes = -41, PublishedAt = DateTime.Now});
        service.AddComment(new Comment
            {Player = "Jojo", CommentText = "LOL", NumberOfLikes = 25, PublishedAt = DateTime.Now});


        service.RemoveComment("Matej");

        Assert.AreEqual(3, service.GetTopComments(10, 0).Count);
        Assert.AreEqual("Jojo", service.GetTopComments(10, 0)[0].Player);
        Assert.AreEqual("bob", service.GetTopComments(10, 0)[2].Player);
    }

    /*
    [TestMethod]
    public void RemoveTestByIdMultiple()
    {
        var service = CreateService();

        service.AddComment(new Comment
        { Id = 1, Player = "Jaro", CommentText = "Something", NumberOfLikes = 100, PublishedAt = DateTime.Now });
        service.AddComment(new Comment
        { Id = 2, Player = "bob", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = DateTime.Now });
        service.AddComment(new Comment
        { Id = 3, Player = "Dio", CommentText = "Good", NumberOfLikes = -41, PublishedAt = DateTime.Now });
        service.AddComment(new Comment
        { Id = 4, Player = "Jojo", CommentText = "LOL", NumberOfLikes = 25, PublishedAt = DateTime.Now });

        service.RemoveComment(1);
        service.RemoveComment(3);

        Assert.AreEqual(2, service.GetTopComments(10, 0).Count);
        Assert.AreEqual("Jojo", service.GetTopComments(10, 0)[0].Player);
        Assert.AreEqual("bob", service.GetTopComments(10, 0)[1].Player);
    }


    [TestMethod]
    public void RemoveTestByIdSingle()
    {
        var service = CreateService();
        service.AddComment(new Comment
        { Id = 1, Player = "Dio", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = DateTime.Now });

        service.RemoveComment(1);

        Assert.AreEqual(0, service.GetTopComments(10, 0).Count);
    }

    [TestMethod]
    public void RemoveTestByIdNotInFile()
    {
        var service = CreateService();

        service.AddComment(new Comment
        { Id = 1, Player = "bob", CommentText = "NO ENJOY", NumberOfLikes = -225, PublishedAt = DateTime.Now });
        service.AddComment(new Comment
        { Id = 2, Player = "Dio", CommentText = "Good", NumberOfLikes = -41, PublishedAt = DateTime.Now });
        service.AddComment(new Comment
        { Id = 3, Player = "Jojo", CommentText = "LOL", NumberOfLikes = 25, PublishedAt = DateTime.Now });


        service.RemoveComment(4);

        Assert.AreEqual(3, service.GetTopComments(10, 0).Count);
        Assert.AreEqual("Jojo", service.GetTopComments(10, 0)[0].Player);
        Assert.AreEqual("bob", service.GetTopComments(10, 0)[2].Player);

    }
    */


    private ICommentService CreateService()
    {
        var service = new CommentServiceEf();
        service.ResetComments();
        return service;
    }
}