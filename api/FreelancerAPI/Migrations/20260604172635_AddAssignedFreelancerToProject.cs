using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedFreelancerToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreelancerId",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreelancerId",
                table: "Projects");
        }
    }
}
