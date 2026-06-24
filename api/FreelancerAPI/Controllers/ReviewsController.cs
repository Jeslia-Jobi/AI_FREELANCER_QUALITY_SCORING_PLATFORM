using FreelancerAPI.Data;
using FreelancerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FreelancerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Freelancer")]
        [HttpGet("my-reviews")]
        public IActionResult GetMyReviews()
        {
            var freelancerId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var reviews = _context.Reviews
                .Where(review => review.FreelancerId == freelancerId)
                .Join(
                    _context.Projects,
                    review => review.ProjectId,
                    project => project.ProjectId,
                    (review, project) => new
                    {
                        project.Title,
                        review.Rating,
                        review.Feedback,
                        review.CreatedAt,
                        review.SentimentScore
                    }
                )
                .ToList();

            return Ok(reviews);
        }
    }
}