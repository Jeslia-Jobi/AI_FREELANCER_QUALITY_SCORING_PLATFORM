using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BCrypt.Net;
using FreelancerAPI.Data;
using FreelancerAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FreelancerAPI.Tests.Integration;

[TestClass]
public class AuthControllerTests
{
    private FreelancerApiFactory _factory = null!;
    private HttpClient _client = null!;

    [TestInitialize]
    public void Setup()
    {
        _factory = new FreelancerApiFactory();
        _client = _factory.CreateClient();

        using var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        db.Users.RemoveRange(db.Users);
        db.SaveChanges();

        db.Users.Add(new User
        {
            Username = "testuser",
            Email = "test@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
            Role = "Client"
        });

        db.SaveChanges();
    }

    [TestMethod]
    public async Task Login_WithValidCredentials_ShouldReturnToken()
    {
        // Arrange
        var request = new
        {
            Username = "testuser",
            Password = "1234"
        };

        // Act
        var response =
            await _client.PostAsJsonAsync(
                "/api/auth/login",
                request);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var json =
            await response.Content.ReadAsStringAsync();

        using var document =
            JsonDocument.Parse(json);

        Assert.IsTrue(
            document.RootElement.TryGetProperty(
                "token",
                out _));
    }
}