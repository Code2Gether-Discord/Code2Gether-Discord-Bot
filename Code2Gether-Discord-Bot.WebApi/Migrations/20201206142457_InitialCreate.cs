using Microsoft.EntityFrameworkCore.Migrations;

namespace Code2Gether_Discord_Bot.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MEMBER",
                columns: table => new
                {
                    MEMBER_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MEMBER_SNOWFLAKE = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEMBER", x => x.MEMBER_ID);
                });

            migrationBuilder.CreateTable(
                name: "PROJECT",
                columns: table => new
                {
                    PROJECT_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PROJECT_NAME = table.Column<string>(type: "TEXT", nullable: false),
                    MEMBER_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT", x => x.PROJECT_ID);
                });

            migrationBuilder.CreateTable(
                name: "MemberProject",
                columns: table => new
                {
                    MembersID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberProject", x => new { x.MembersID, x.ProjectsID });
                    table.ForeignKey(
                        name: "FK_MemberProject_MEMBER_MembersID",
                        column: x => x.MembersID,
                        principalTable: "MEMBER",
                        principalColumn: "MEMBER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberProject_PROJECT_ProjectsID",
                        column: x => x.ProjectsID,
                        principalTable: "PROJECT",
                        principalColumn: "PROJECT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberProject_ProjectsID",
                table: "MemberProject",
                column: "ProjectsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberProject");

            migrationBuilder.DropTable(
                name: "MEMBER");

            migrationBuilder.DropTable(
                name: "PROJECT");
        }
    }
}
