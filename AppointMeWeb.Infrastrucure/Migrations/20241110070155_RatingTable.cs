using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointMeWeb.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class RatingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Rating Id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evaluation = table.Column<int>(type: "int", nullable: false, comment: "User evaluation for service"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "User comment"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_BusinessServiceProviders_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "BusinessServiceProviders",
                        principalColumn: "Id");
                },
                comment: "User business rating");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ApplicationUserId",
                table: "Ratings",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BusinessId",
                table: "Ratings",
                column: "BusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");
        }
    }
}
