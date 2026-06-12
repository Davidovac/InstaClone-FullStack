using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserContextCaseFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollowers");

            migrationBuilder.CreateTable(
                name: "user_followers",
                columns: table => new
                {
                    follower_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    following_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_followers", x => new { x.follower_id, x.following_id });
                    table.ForeignKey(
                        name: "FK_user_followers_asp_net_users_follower_id",
                        column: x => x.follower_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_followers_asp_net_users_following_id",
                        column: x => x.following_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_followers_following_id",
                table: "user_followers",
                column: "following_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_followers");

            migrationBuilder.CreateTable(
                name: "UserFollowers",
                columns: table => new
                {
                    FollowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowers", x => new { x.FollowerId, x.FollowingId });
                    table.ForeignKey(
                        name: "FK_UserFollowers_asp_net_users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "asp_net_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserFollowers_asp_net_users_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowingId",
                table: "UserFollowers",
                column: "FollowingId");
        }
    }
}
