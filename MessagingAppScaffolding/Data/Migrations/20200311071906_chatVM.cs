using Microsoft.EntityFrameworkCore.Migrations;

namespace MessagingApp.Data.Migrations
{
    public partial class chatVM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UnixTimeStamp",
                table: "ChatRooms",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnixTimeStamp",
                table: "ChatRooms");
        }
    }
}
