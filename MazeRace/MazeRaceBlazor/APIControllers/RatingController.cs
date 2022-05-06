using MazeRaceCore.Entity;
using MazeRaceCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace MazeRaceBlazor.APIControllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService = new RatingServiceEf();

    //GET: /api/Rating
    [HttpGet]
    public int GetAverageRatings()
    {
        return _ratingService.GetAverageRating();
    }


    [HttpGet("list")]
    public IEnumerable<Rating> GetRatings()
    {
        return _ratingService.GetAllRatings();
    }


    [HttpPost]
    public void PostRating([FromBody] Rating rating)
    {
        _ratingService.AddRating(rating);
    }
}