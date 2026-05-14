namespace FreelancerAPI.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public int ClientId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Budget { get; set; }

        public DateTime Deadline { get; set; }

        public string Status { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
    }
}