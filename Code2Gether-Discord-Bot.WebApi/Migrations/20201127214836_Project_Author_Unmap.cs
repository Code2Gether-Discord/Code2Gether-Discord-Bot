using Microsoft.EntityFrameworkCore.Migrations;

namespace Code2Gether_Discord_Bot.WebApi.Migrations
{
    public partial class Project_Author_Unmap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_AuthorID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AuthorID",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Projects",
                newName: "AuthorId");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Projects",
                newName: "AuthorID");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorID",
                table: "Projects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AuthorID",
                table: "Projects",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_AuthorID",
                table: "Projects",
                column: "AuthorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
