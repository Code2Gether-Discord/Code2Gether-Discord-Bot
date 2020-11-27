using Microsoft.EntityFrameworkCore.Migrations;

namespace Code2Gether_Discord_Bot.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectRoles",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<long>(type: "INTEGER", nullable: false),
                    CanReadData = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanWriteData = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    AuthorID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projects_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AuthorID",
                table: "Projects",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectID",
                table: "Users",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Projects_ProjectID",
                table: "Users",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_AuthorID",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
