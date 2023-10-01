using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class NewTableProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chat_ChatLink",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatLink",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "ChatLink",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChatLink",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    ChatLink = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatLink);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatLink",
                table: "Messages",
                column: "ChatLink");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chat_ChatLink",
                table: "Messages",
                column: "ChatLink",
                principalTable: "Chat",
                principalColumn: "ChatLink",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
