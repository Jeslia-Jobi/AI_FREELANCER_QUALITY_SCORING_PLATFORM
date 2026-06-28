using FreelancerAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FreelancerAPI.Tests.Services;

[TestClass]
public class ScoreServiceTests
{
    private readonly ScoreService _scoreService = new();

    [TestMethod]
    public void CalculateOverallScore_ShouldReturn100_ForExcellentFreelancer()
    {
        // Arrange
        double rating = 5.0;
        int completedProjects = 20;
        double sentiment = 10.0;

        DateTime reviewDate = new DateTime(2026, 1, 1);
        DateTime deadline = new DateTime(2026, 1, 5);

        // Act
        double score = _scoreService.CalculateOverallScore(
            rating,
            completedProjects,
            sentiment,
            reviewDate,
            deadline);

        // Assert
        Assert.AreEqual(100, score);
    }

    [TestMethod]
    public void CalculateOverallScore_ShouldApplyLatePenalty()  
    {
        // Arrange
        double rating = 4.0;
        int completedProjects = 5;
        double sentiment = 5.0;

        DateTime deadline = new DateTime(2026, 1, 1);
        DateTime reviewDate = new DateTime(2026, 1, 6);

        // Act
        double scoreWithoutPenalty = _scoreService.CalculateOverallScore(
            rating,
            completedProjects,
            sentiment,
            deadline,
            deadline);

        double scoreWithPenalty = _scoreService.CalculateOverallScore(
            rating,
            completedProjects,
            sentiment,
            reviewDate,
            deadline);

        // Assert
        Assert.IsTrue(scoreWithPenalty < scoreWithoutPenalty);
    }
    
    [TestMethod]
    public void CalculateOverallScore_ShouldReturnZero_WhenScoreBecomesNegative()
    {
        // Arrange
        double rating = 0;
        int completedProjects = 0;
        double sentiment = -10;

        DateTime deadline = new DateTime(2026, 1, 1);
        DateTime reviewDate = new DateTime(2026, 2, 15);

        // Act
        double score = _scoreService.CalculateOverallScore(
            rating,
            completedProjects,
            sentiment,
            reviewDate,
            deadline);

        // Assert
        Assert.AreEqual(0, score);
    }

    [TestMethod]
    public void CalculateOverallScore_ShouldIncrease_WhenCompletedProjectsIncrease()
    {
        // Arrange
        double rating = 4;
        double sentiment = 5;

        // Act
        double lowScore = _scoreService.CalculateOverallScore(
            rating,
            2,
            sentiment,
            DateTime.Today,
            DateTime.Today);

        double highScore = _scoreService.CalculateOverallScore(
            rating,
            10,
            sentiment,
            DateTime.Today,
            DateTime.Today);

        // Assert
        Assert.IsTrue(highScore > lowScore);
    }

    [TestMethod]
    public void CalculateOverallScore_ShouldIncrease_WhenSentimentImproves()
    {
        // Arrange
        double rating = 4;
        int completedProjects = 5;

        // Act
        double negative = _scoreService.CalculateOverallScore(
            rating,
            completedProjects,
            -5,
            DateTime.Today,
            DateTime.Today);

        double positive = _scoreService.CalculateOverallScore(
            rating,
            completedProjects,
            8,
            DateTime.Today,
            DateTime.Today);

        // Assert
        Assert.IsTrue(positive > negative);
    }
}