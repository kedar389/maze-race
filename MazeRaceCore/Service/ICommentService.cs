using MazeRaceCore.Entity;

namespace MazeRaceCore.Service;

public interface ICommentService
{
    void AddComment(Comment comment);

    IList<Comment> GetTopComments(int number, int offset);

    IList<Comment> GetNewestComments(int number, int offset);

    void ResetComments();

    void RemoveComment(string userName);

    void RemoveComment(int commentId);

    void LikeComment(int commentId);

    void DislikeComment(int commentId);
}