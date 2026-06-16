public class Review
{
    public int ReviewId { get; set; }

    public int ProjectId { get; set; }

    public int FreelancerId { get; set; }

    public int Rating { get; set; }

    public string Feedback { get; set; }

    public DateTime CreatedAt{get; set;}
    public double SentimentScore { get; set; }
}