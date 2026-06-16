using System.ComponentModel.DataAnnotations;

namespace FreelancerAPI.Models
{
    public class ProjectApplication
    {
        [Key]
        public int ApplicationId { get; set; }

        public int ProjectId { get; set; }

        public int FreelancerId { get; set; }

        public DateTime AppliedAt { get; set; }

        public string Status { get; set; } = "Pending";
    }
}