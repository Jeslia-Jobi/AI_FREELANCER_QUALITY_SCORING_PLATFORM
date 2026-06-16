using Microsoft.EntityFrameworkCore;
using FreelancerAPI.Models;

namespace FreelancerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<FreelancerProfile> FreelancerProfiles { get; set; }

        public DbSet<ProjectApplication> ProjectApplications { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Review> Reviews
        {
            get;
            set;
        }
    }
}