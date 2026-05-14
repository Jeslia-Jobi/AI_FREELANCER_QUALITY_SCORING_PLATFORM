using FreelancerAPI.Data;
using FreelancerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    }
}