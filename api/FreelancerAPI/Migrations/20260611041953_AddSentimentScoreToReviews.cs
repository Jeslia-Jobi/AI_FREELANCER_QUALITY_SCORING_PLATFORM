using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSentimentScoreToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SentimentScore",
                table: "Reviews",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentimentScore",
                table: "Reviews");
        }
    }
}
