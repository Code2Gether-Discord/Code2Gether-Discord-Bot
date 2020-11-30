using Microsoft.EntityFrameworkCore.Migrations;

namespace Code2Gether_Discord_Bot.WebApi.Migrations
{
    public partial class MoreForeignKey3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_MEMBER_ID",
                table: "PROJECT",
                column: "MEMBER_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PROJECT_MEMBER_MEMBER_ID",
                table: "PROJECT",
                column: "MEMBER_ID",
                principalTable: "MEMBER",
                principalColumn: "MEMBER_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROJECT_MEMBER_MEMBER_ID",
                table: "PROJECT");

            migrationBuilder.DropIndex(
                name: "IX_PROJECT_MEMBER_ID",
                table: "PROJECT");
        }
    }
}
