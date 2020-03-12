using Microsoft.EntityFrameworkCore.Migrations;

namespace MessagingApp.Data.Migrations
{
    public partial class interfaceModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "UserNameSignature",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserNameSignature",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "PosterId",
                table: "Replies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChatRoomID",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterId",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    ChatRoomID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.ChatRoomID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_PosterId",
                table: "Replies",
                column: "PosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomID",
                table: "Messages",
                column: "ChatRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PosterId",
                table: "Messages",
                column: "PosterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatRooms_ChatRoomID",
                table: "Messages",
                column: "ChatRoomID",
                principalTable: "ChatRooms",
                principalColumn: "ChatRoomID",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatRooms_ChatRoomID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_PosterId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_PosterId",
                table: "Replies");

            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_Replies_PosterId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatRoomID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PosterId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PosterId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ChatRoomID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PosterId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Replies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameSignature",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameSignature",
                table: "Messages",
                type: "nvarchar(max)",
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
    }
}
