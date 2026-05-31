namespace backend.DTOs
{
    public class RecommendationDto
    {
        public int FreelancerId { get; set; }

        public int UserId { get; set; }
        
        public string Username { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string Skills { get; set; } = string.Empty;

        public double MatchPercentage { get; set; }
    }
}