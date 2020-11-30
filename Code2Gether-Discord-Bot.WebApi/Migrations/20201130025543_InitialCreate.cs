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
                    PROJECT_NAME = table.Column<string>(type: "TEXT", nullable: true),
                    MEMBER_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT", x => x.PROJECT_ID);
                    table.ForeignKey(
                        name: "FK_PROJECT_MEMBER_MEMBER_ID",
                        column: x => x.MEMBER_ID,
                        principalTable: "MEMBER",
                        principalColumn: "MEMBER_ID",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_PROJECT_MEMBER_MEMBER_MEMBER_ID",
                        column: x => x.MEMBER_ID,
                        principalTable: "MEMBER",
                        principalColumn: "MEMBER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROJECT_MEMBER_PROJECT_PROJECT_ID",
                        column: x => x.PROJECT_ID,
                        principalTable: "PROJECT",
                        principalColumn: "PROJECT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_MEMBER_ID",
                table: "PROJECT",
                column: "MEMBER_ID");

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
                name: "PROJECT");

            migrationBuilder.DropTable(
                name: "MEMBER");
        }
    }
}
