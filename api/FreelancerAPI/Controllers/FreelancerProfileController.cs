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
    public class FreelancerProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FreelancerProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateOrUpdateProfile(FreelancerProfile profile)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim);

            var existingProfile = _context.FreelancerProfiles
                .FirstOrDefault(p => p.UserId == userId);

            if (existingProfile == null)
            {
                profile.UserId = userId;

                _context.FreelancerProfiles.Add(profile);
            }
            else
            {
                existingProfile.Bio = profile.Bio;
                existingProfile.Skills = profile.Skills;
                existingProfile.Experience = profile.Experience;

                existingProfile.UpdatedAt = DateTime.Now;
            }

            _context.SaveChanges();

            var savedProfile = _context.FreelancerProfiles
                .FirstOrDefault(p => p.UserId == userId);

            return Ok(savedProfile);
        }

        [HttpGet]
        public IActionResult GetMyProfile()
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var profile = _context.FreelancerProfiles
                .FirstOrDefault(p => p.UserId == userId);

            return Ok(profile);
        }
    }
}