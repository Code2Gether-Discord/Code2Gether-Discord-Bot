using Microsoft.EntityFrameworkCore.Migrations;

namespace Code2Gether_Discord_Bot.WebApi.Migrations
{
    public partial class ProjectMemberForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_MEMBER_MEMBER_ID",
                table: "PROJECT_MEMBER",
                column: "MEMBER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_MEMBER_PROJECT_ID",
                table: "PROJECT_MEMBER",
                column: "PROJECT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PROJECT_MEMBER_MEMBER_MEMBER_ID",
                table: "PROJECT_MEMBER",
                column: "MEMBER_ID",
                principalTable: "MEMBER",
                principalColumn: "MEMBER_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PROJECT_MEMBER_PROJECT_PROJECT_ID",
                table: "PROJECT_MEMBER",
                column: "PROJECT_ID",
                principalTable: "PROJECT",
                principalColumn: "PROJECT_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROJECT_MEMBER_MEMBER_MEMBER_ID",
                table: "PROJECT_MEMBER");

            migrationBuilder.DropForeignKey(
                name: "FK_PROJECT_MEMBER_PROJECT_PROJECT_ID",
                table: "PROJECT_MEMBER");

            migrationBuilder.DropIndex(
                name: "IX_PROJECT_MEMBER_MEMBER_ID",
                table: "PROJECT_MEMBER");

            migrationBuilder.DropIndex(
                name: "IX_PROJECT_MEMBER_PROJECT_ID",
                table: "PROJECT_MEMBER");
        }
    }
}
