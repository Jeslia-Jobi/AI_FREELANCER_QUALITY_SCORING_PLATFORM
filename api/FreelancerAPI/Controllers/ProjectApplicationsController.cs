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
    public class ProjectApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Freelancer")]
        [HttpPost("{projectId}")]
        public IActionResult ApplyForProject(int projectId)
        {
           
            var freelancerId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

             var alreadyApplied = _context.ProjectApplications.Any(
                a => a.ProjectId == projectId &&
                    a.FreelancerId == freelancerId
            );

            if (alreadyApplied)
            {
                return BadRequest("You have already applied.");
            }
            

            var application = new ProjectApplication
            {
                ProjectId = projectId,
                FreelancerId = freelancerId,
                AppliedAt = DateTime.UtcNow,
                Status = "Pending"
            };

            _context.ProjectApplications.Add(application);
            _context.SaveChanges();

            return Ok(application);
        }
    }
}