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
            return Ok(_context.Projects.ToList());
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

            var freelancers = _context.FreelancerProfiles.ToList();

            var recommendations = freelancers
                .Select(f =>
                {
                    var freelancerSkills = f.Skills
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
                        FreelancerId = f.ProfileId,
                        UserId = f.UserId,
                        Bio = f.Bio,
                        Skills = f.Skills,
                        MatchPercentage = Math.Round(matchPercentage, 2)
                    };
                })
                .Where(r => r.MatchPercentage > 0)
                .OrderByDescending(r => r.MatchPercentage)
                .ToList();

            return Ok(recommendations);
        }
    }
}