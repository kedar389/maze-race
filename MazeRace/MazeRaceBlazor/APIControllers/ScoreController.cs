using MazeRaceCore.Entity;
using MazeRaceCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace MazeRaceBlazor.APIControllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class ScoreController : ControllerBase
{
    private readonly IScoreService _scoreService = new ScoreServiceEf();

    //GET: /api/Score/endless
    [HttpGet]
    public IEnumerable<Score> GetScores()
    {
        return _scoreService.GetTopScores(10, 0, "endless");
    }


    //POST: /api/Score
    [HttpPost]
    public void PostScore([FromBody] Score score)
    {
        score.PlayedAt = DateTime.Now;
        _scoreService.AddScore(score);
    }
}