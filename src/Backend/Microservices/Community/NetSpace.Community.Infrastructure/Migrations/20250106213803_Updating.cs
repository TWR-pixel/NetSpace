using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NetSpace.Community.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityEntityUserEntity");

            migrationBuilder.CreateTable(
                name: "CommunitySubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubscriberId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommunityId = table.Column<int>(type: "integer", nullable: false),
                    SubscribingStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunitySubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunitySubscriptions_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunitySubscriptions_Users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySubscriptions_CommunityId",
                table: "CommunitySubscriptions",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySubscriptions_SubscriberId",
                table: "CommunitySubscriptions",
                column: "SubscriberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunitySubscriptions");

            migrationBuilder.CreateTable(
                name: "CommunityEntityUserEntity",
                columns: table => new
                {
                    CommunitySubscribersId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommunitySubscriptionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityEntityUserEntity", x => new { x.CommunitySubscribersId, x.CommunitySubscriptionsId });
                    table.ForeignKey(
                        name: "FK_CommunityEntityUserEntity_Communities_CommunitySubscription~",
                        column: x => x.CommunitySubscriptionsId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityEntityUserEntity_Users_CommunitySubscribersId",
                        column: x => x.CommunitySubscribersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityEntityUserEntity_CommunitySubscriptionsId",
                table: "CommunityEntityUserEntity",
                column: "CommunitySubscriptionsId");
        }
    }
}
