using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialConnect.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFollowers",
                table: "UsersFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UsersFollowers_FollowedUserId",
                table: "UsersFollowers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersFollowers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFollowers",
                table: "UsersFollowers",
                columns: new[] { "FollowedUserId", "FollowerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFollowers",
                table: "UsersFollowers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersFollowers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFollowers",
                table: "UsersFollowers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFollowers_FollowedUserId",
                table: "UsersFollowers",
                column: "FollowedUserId");
        }
    }
}
