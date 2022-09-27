using MazeRaceCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace MazeRaceCore.Service;

public class CommentServiceEf : ICommentService
{
    public void AddComment(Comment comment)
    {
        using var context = new MazeRaceDbContext();
        context.Comments.Add(comment);
        context.SaveChanges();
    }


    public IList<Comment> GetTopComments(int number, int offset)
    {
        using var context = new MazeRaceDbContext();
        return (from s in context.Comments orderby s.NumberOfLikes descending select s).Skip(offset).Take(number)
            .ToList();
    }


    public IList<Comment> GetNewestComments(int number, int offset)
    {
        using var context = new MazeRaceDbContext();
        return (from s in context.Comments orderby s.PublishedAt descending select s).Skip(offset).Take(number)
            .ToList();
    }

    public void ResetComments()
    {
        using var context = new MazeRaceDbContext();
        context.Database.ExecuteSqlRaw("DELETE FROM Comments");
    }


    public void RemoveComment(string userName)
    {
        using var context = new MazeRaceDbContext();
        var commentToRemove = context.Comments.SingleOrDefault(s => s.Player == userName);

        if (commentToRemove != null)
        {
            context.Comments.Remove(commentToRemove);
            context.SaveChanges();
        }
    }

    public void RemoveComment(int commentId)
    {
        using var context = new MazeRaceDbContext();
        var commentToRemove = context.Comments.SingleOrDefault(s => s.Id == commentId);

        if (commentToRemove != null)
        {
            context.Comments.Remove(commentToRemove);
            context.SaveChanges();
        }
    }

    public void LikeComment(int commentId)
    {
        ModifierLikeComment(commentId, 1);
    }

    public void DislikeComment(int commentId)
    {
        ModifierLikeComment(commentId, -1);
    }


    private void ModifierLikeComment(int commentId, int number)
    {
        using var context = new MazeRaceDbContext();
        var comment = context.Comments.SingleOrDefault(s => s.Id == commentId);

        if (comment != null)
        {
            comment.NumberOfLikes += number;
            context.SaveChanges();
        }
    }
}