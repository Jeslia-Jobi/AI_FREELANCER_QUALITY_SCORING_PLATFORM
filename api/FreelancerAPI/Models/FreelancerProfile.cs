using System.ComponentModel.DataAnnotations;

namespace FreelancerAPI.Models
{
    public class FreelancerProfile
    {
        [Key]
        public int ProfileId { get; set; }

        public int UserId { get; set; }

        public string Bio { get; set; } = string.Empty;

        public string Skills { get; set; } = string.Empty;

        public string Experience { get; set; } = string.Empty;

        public string Education { get; set; } = string.Empty;

        public string Availability { get; set; } = string.Empty;

        public double OverallScore { get; set; } = 50;

        public int CompletedProjects { get; set; } = 0;

        public double Rating { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
    }
}