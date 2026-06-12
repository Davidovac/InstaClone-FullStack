using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMajorEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "asp_net_users");

            migrationBuilder.AddColumn<string>(
                name: "profile_desc",
                table: "asp_net_users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "profile_picture",
                table: "asp_net_users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_posts_asp_net_users_author_id",
                        column: x => x.author_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_asp_net_users_author_id",
                        column: x => x.author_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    liker_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_likes", x => x.id);
                    table.ForeignKey(
                        name: "FK_likes_asp_net_users_liker_id",
                        column: x => x.liker_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likes_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reply_comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    replied_comment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reply_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_reply_comments_asp_net_users_author_id",
                        column: x => x.author_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reply_comments_comments_replied_comment_id",
                        column: x => x.replied_comment_id,
                        principalTable: "comments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_comments_author_id",
                table: "comments",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_post_id",
                table: "comments",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_likes_liker_id",
                table: "likes",
                column: "liker_id");

            migrationBuilder.CreateIndex(
                name: "ix_likes_post_id",
                table: "likes",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_author_id",
                table: "posts",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_reply_comments_author_id",
                table: "reply_comments",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_reply_comments_replied_comment_id",
                table: "reply_comments",
                column: "replied_comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowingId",
                table: "UserFollowers",
                column: "FollowingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "reply_comments");

            migrationBuilder.DropTable(
                name: "UserFollowers");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropColumn(
                name: "profile_desc",
                table: "asp_net_users");

            migrationBuilder.DropColumn(
                name: "profile_picture",
                table: "asp_net_users");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "asp_net_users",
                type: "datetime2",
                nullable: true);
        }
    }
}
