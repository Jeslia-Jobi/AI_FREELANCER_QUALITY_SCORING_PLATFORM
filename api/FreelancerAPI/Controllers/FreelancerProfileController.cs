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
        public IActionResult CreateProfile(FreelancerProfile profile)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            profile.UserId = userId;

            _context.FreelancerProfiles.Add(profile);

            _context.SaveChanges();

            return Ok(profile);
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