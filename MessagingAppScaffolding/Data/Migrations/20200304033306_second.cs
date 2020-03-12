using Microsoft.EntityFrameworkCore.Migrations;

namespace MessagingApp.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_PosterId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_PosterId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_PosterId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PosterId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PosterId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "PosterId",
                table: "Messages");

            migrationBuilder.AlterColumn<long>(
                name: "UnixTimeStamp",
                table: "Replies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Replies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Replies",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UnixTimeStamp",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_AppUserId",
                table: "Replies",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AppUserId",
                table: "Messages",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AppUserId",
                table: "Messages",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_AppUserId",
                table: "Replies",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AppUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_AppUserId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_AppUserId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AppUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "UnixTimeStamp",
                table: "Replies",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "PosterId",
                table: "Replies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnixTimeStamp",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "PosterId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_PosterId",
                table: "Replies",
                column: "PosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PosterId",
                table: "Messages",
                column: "PosterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_PosterId",
                table: "Messages",
                column: "PosterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_PosterId",
                table: "Replies",
                column: "PosterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
