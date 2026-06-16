using FreelancerAPI.Data;
using FreelancerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using backend.DTOs;

namespace FreelancerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            var clientId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            project.ClientId = clientId;
            project.Status = "Open";

            _context.Projects.Add(project);
            _context.SaveChanges();

            return Ok(project);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("my-projects")]
        public IActionResult GetMyProjects()
        {
            var clientId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );
            Console.WriteLine(clientId);
            var projects = _context.Projects
                .Where(p => p.ClientId == clientId)
                .ToList();

            return Ok(projects);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _context.Projects
                .Where(p => p.Status == "Open")
                .ToList();

            return Ok(projects);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            var project = _context.Projects
                .FirstOrDefault(
                    p => p.ProjectId == id
                );

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet("{projectId}/recommendations")]
        public IActionResult GetRecommendations(int projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == projectId);

            if (project == null)
            {
                return NotFound("Project not found");
            }

            var requiredSkills = project.Requirements
                .ToLower()
                .Split(',')
                .Select(s => s.Trim())
                .ToList();

           var freelancerData = _context.FreelancerProfiles
                .Join(
                    _context.Users,
                    freelancer => freelancer.UserId,
                    user => user.Id,
                    (freelancer, user) => new
                    {
                        Freelancer = freelancer,
                        Username = user.Username
                    }
                )
                .ToList();

            var recommendations = freelancerData
                .Select(f =>
                {
                    var freelancerSkills = f.Freelancer.Skills
                        .ToLower()
                        .Split(',')
                        .Select(s => s.Trim())
                        .ToList();

                    int matchedSkills = requiredSkills
                        .Count(skill => freelancerSkills.Contains(skill));

                    double matchPercentage =
                        ((double)matchedSkills / requiredSkills.Count) * 100;

                    return new RecommendationDto
                    {
                        FreelancerId = f.Freelancer.ProfileId,
                        UserId = f.Freelancer.UserId,
                        Username = f.Username,
                        Bio = f.Freelancer.Bio,
                        Skills = f.Freelancer.Skills,
                        MatchPercentage = Math.Round(matchPercentage, 2)
                    };
                })
                .Where(r => r.MatchPercentage >= 50)
                .OrderByDescending(r => r.MatchPercentage)
                .ToList();

            return Ok(recommendations);
        }

        [Authorize(Roles = "Freelancer")]
        [HttpGet("assigned")]
        public IActionResult GetAssignedProjects()
        {
            var freelancerId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var projects = _context.Projects
                .Where(p =>
                    p.FreelancerId == freelancerId &&
                    p.Status == "Assigned"
                )
                .ToList();

            return Ok(projects);
        }

        [Authorize(Roles = "Freelancer")]
        [HttpGet("completed")]
        public IActionResult GetCompletedProjects()
        {
            var freelancerId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var projects = _context.Projects
                .Where(p =>
                    p.FreelancerId == freelancerId &&
                    p.Status == "Completed"
                )
                .ToList();

            return Ok(projects);
        }

        [Authorize(Roles = "Freelancer")]
        [HttpPut("{projectId}/request-completion")]
        public IActionResult RequestCompletion(
            int projectId)
        {
            var freelancerId = int.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier
                )?.Value
            );

            var project = _context.Projects
                .FirstOrDefault(
                    p => p.ProjectId == projectId
                    && p.FreelancerId == freelancerId
                );

            if (project == null)
            {
                return NotFound();
            }

            project.Status =
                "Completion Requested";

            _context.SaveChanges();

            return Ok();
        }

        [Authorize(Roles = "Client")]
        [HttpPost("review")]
        public IActionResult SubmitReview(
            Review review)
        {
            review.CreatedAt = DateTime.UtcNow;

            _context.Reviews.Add(review);


            var project = _context.Projects
                .FirstOrDefault(
                    p => p.ProjectId == review.ProjectId
                );

            if (project != null)
            {
                project.Status = "Completed";
            }

            var profile = _context.FreelancerProfiles
                .FirstOrDefault(
                    p => p.UserId == review.FreelancerId
                );

            if (profile != null)
            {
                profile.CompletedProjects++;

                _context.SaveChanges();

                profile.Rating = _context.Reviews
                    .Where(
                        r => r.FreelancerId ==
                        review.FreelancerId
                    )
                    .Average(
                        r => r.Rating
                    );

                profile.OverallScore = Math.Min(100,
                    (profile.Rating * 20)+(profile.CompletedProjects * 2)
                );
            }

            _context.SaveChanges();

            return Ok(new
            {
                Message = "Review submitted successfully"
            });
        }
    }
}