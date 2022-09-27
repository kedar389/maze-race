using MazeRaceCore.Entity;
using MazeRaceCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace MazeRaceBlazor.APIControllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService = new CommentServiceEf();

    //api/Comment
    [HttpGet]
    public IEnumerable<Comment> GetComments()
    {
        return _commentService.GetTopComments(10, 0);
    }


    [HttpPost]
    public void PostComment(Comment comment)
    {
        comment.PublishedAt = DateTime.Now;
        comment.NumberOfLikes = 0;
        _commentService.AddComment(comment);
    }
}