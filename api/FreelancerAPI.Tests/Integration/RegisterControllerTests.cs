using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FreelancerAPI.Data;
using FreelancerAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FreelancerAPI.Tests.Integration;

[TestClass]
public class RegisterControllerTests
{
    private FreelancerApiFactory _factory = null!;
    private HttpClient _client = null!;

    [TestInitialize]
    public void Setup()
    {
        _factory = new FreelancerApiFactory();
        _client = _factory.CreateClient();
    }

    [TestMethod]
    public async Task Register_WithValidUser_ShouldReturnOk_AndSaveUser()
    {
        // Arrange
        var user = new User
        {
            Username = "newclient",
            Email = "newclient@test.com",
            PasswordHash = "123456",
            Role = "Client"
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/auth/register",
            user);

        // Assert response
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert database
        using var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var savedUser = db.Users.FirstOrDefault(u =>
            u.Username == "newclient");

        savedUser.Should().NotBeNull();

        savedUser!.PasswordHash.Should().NotBe("123456");
    }
}