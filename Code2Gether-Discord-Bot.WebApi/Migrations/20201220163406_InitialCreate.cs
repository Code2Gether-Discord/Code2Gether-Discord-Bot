using Microsoft.EntityFrameworkCore.Migrations;

namespace Code2Gether_Discord_Bot.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MEMBERS",
                columns: table => new
                {
                    MEMBER_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MEMBER_SNOWFLAKE_ID = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEMBERS", x => x.MEMBER_ID);
                });

            migrationBuilder.CreateTable(
                name: "PROJECTS",
                columns: table => new
                {
                    PROJECT_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PROJECT_NAME = table.Column<string>(type: "TEXT", nullable: false),
                    AUTHOR_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECTS", x => x.PROJECT_ID);
                });

            migrationBuilder.CreateTable(
                name: "PROJECT_MEMBER",
                columns: table => new
                {
                    PROJECT_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    MEMBER_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT_MEMBER", x => new { x.MEMBER_ID, x.PROJECT_ID });
                    table.ForeignKey(
                        name: "FK_PROJECT_MEMBER_MEMBERS_MEMBER_ID",
                        column: x => x.MEMBER_ID,
                        principalTable: "MEMBERS",
                        principalColumn: "MEMBER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROJECT_MEMBER_PROJECTS_PROJECT_ID",
                        column: x => x.PROJECT_ID,
                        principalTable: "PROJECTS",
                        principalColumn: "PROJECT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_MEMBER_PROJECT_ID",
                table: "PROJECT_MEMBER",
                column: "PROJECT_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROJECT_MEMBER");

            migrationBuilder.DropTable(
                name: "MEMBERS");

            migrationBuilder.DropTable(
                name: "PROJECTS");
        }
    }
}
