namespace FreelancerAPI.Services;

public class ScoreService
{
    public double CalculateOverallScore(
        double rating,
        int completedProjects,
        double averageSentiment,
        DateTime reviewDate,
        DateTime deadline)
    {
        double ratingScore = rating * 15;

        double completionScore = completedProjects * 2;

        double sentimentScore =
            ((averageSentiment / 10.0) + 1.0) * 15.0;

        double latePenalty = 0;

        if (reviewDate > deadline)
        {
            var daysLate = (reviewDate - deadline).TotalDays;

            // 2 points per day, maximum 20
            latePenalty = Math.Min(20, daysLate * 2);
        }

        return Math.Max(
            0,
            Math.Min(
                100,
                ratingScore +
                completionScore +
                sentimentScore -
                latePenalty
            )
        );
    }
}