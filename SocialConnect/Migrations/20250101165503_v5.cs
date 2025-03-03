using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialConnect.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFollowerId",
                table: "UsersFollowers");

            migrationBuilder.AddColumn<string>(
                name: "FollowId",
                table: "UsersFollowers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowId",
                table: "UsersFollowers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserFollowerId",
                table: "UsersFollowers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
