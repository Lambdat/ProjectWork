using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectWork.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserSsn",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserSsn",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "UserSsn",
                table: "Posts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(767)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserSsn",
                table: "Posts",
                type: "varchar(767)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserSsn",
                table: "Posts",
                column: "UserSsn");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserSsn",
                table: "Posts",
                column: "UserSsn",
                principalTable: "Users",
                principalColumn: "Ssn",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
