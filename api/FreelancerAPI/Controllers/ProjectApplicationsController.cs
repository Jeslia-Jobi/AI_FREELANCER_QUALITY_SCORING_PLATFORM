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

            var project = _context.Projects
                .FirstOrDefault(p => p.ProjectId == projectId);

            if (project == null)
            {
                return NotFound();
            }

            if (project.Status == "Assigned")
            {
                return BadRequest("Project already assigned.");
            }

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

        [Authorize(Roles = "Client")]
        [HttpGet("project/{projectId}")]
        public IActionResult GetApplicants(int projectId)
        {
            var applicants = _context.ProjectApplications
                .Where(a => a.ProjectId == projectId)
                .Join(
                    _context.Users,
                    application => application.FreelancerId,
                    user => user.Id,
                    (application, user) => new
                    {
                        application.ApplicationId,
                        application.ProjectId,
                        application.Status,
                        application.AppliedAt,
                        user.Id,
                        user.Username
                    }
                )
                .ToList();

            return Ok(applicants);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("proposals")]
        public IActionResult GetMyProjectApplications()
        {
            var clientId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var result = _context.Projects
                .Where(p => p.ClientId == clientId)
                .Select(project => new
                {
                    project.ProjectId,
                    project.Title,

                    Applicants = _context.ProjectApplications
                    .Where(a => a.ProjectId == project.ProjectId)
                    .Join(
                        _context.Users,
                        a => a.FreelancerId,
                        u => u.Id,
                        (a, u) => new
                        {
                            Application = a,
                            User = u
                        }
                    )
                    .Join(
                        _context.FreelancerProfiles,
                        x => x.User.Id,
                        p => p.UserId,
                        (x, p) => new
                        {
                            x.Application.ApplicationId,
                            x.Application.Status,

                            x.User.Id,
                            x.User.Username,

                            p.OverallScore,
                            p.CompletedProjects,
                            p.Rating,
                            p.Skills,
                            p.Bio
                        }
                    )
                    .ToList()
                })
                .ToList();

            return Ok(result);
        }
        
        [Authorize(Roles = "Client")]
        [HttpPut("accept/{applicationId}")]
        public IActionResult AcceptApplication(
            int applicationId)
        {
            var application =
                _context.ProjectApplications
                .FirstOrDefault(
                    a => a.ApplicationId ==
                        applicationId
                );

            if (application == null)
            {
                return NotFound();
            }

            application.Status = "Accepted";

            var project = _context.Projects
                .FirstOrDefault(
                    p => p.ProjectId ==
                        application.ProjectId
                );

            if (project != null)
            {
                project.Status = "Assigned";
                project.FreelancerId =
                    application.FreelancerId;
            }

            var others =
                _context.ProjectApplications
                .Where(a =>
                    a.ProjectId ==
                    application.ProjectId &&
                    a.ApplicationId !=
                    applicationId)
                .ToList();

            foreach (var app in others)
            {
                app.Status = "Rejected";
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}