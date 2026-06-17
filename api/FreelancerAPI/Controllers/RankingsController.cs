using FreelancerAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RankingsController(
            AppDbContext context
        )
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRankings()
        {
            var rankings = _context.FreelancerProfiles
                .Join(
                    _context.Users,
                    profile => profile.UserId,
                    user => user.Id,
                    (profile, user) => new
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        OverallScore = profile.OverallScore,
                        Rating = profile.Rating,
                        CompletedProjects =
                            profile.CompletedProjects,
                        Skills = profile.Skills,
                        Experience = profile.Experience
                    }
                )
                .OrderByDescending(x => x.OverallScore)
                .ThenByDescending(x => x.Rating)
                .ThenByDescending(x => x.CompletedProjects)
                .ThenByDescending(x => x.Experience)
                .ToList();

            return Ok(rankings);
        }
    }
}