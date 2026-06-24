using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSentimentCategoryToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SentimentCategory",
                table: "Reviews",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentimentCategory",
                table: "Reviews");
        }
    }
}
