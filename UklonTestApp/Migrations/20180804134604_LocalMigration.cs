using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UklonTestApp.Migrations
{
    public partial class LocalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegionCode = table.Column<string>(nullable: true),
                    RegionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegionTrafficStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegionId = table.Column<Guid>(nullable: true),
                    DateTimeNow = table.Column<DateTimeOffset>(nullable: false),
                    TrafficLevel = table.Column<string>(nullable: true),
                    TrafficIcon = table.Column<string>(nullable: true),
                    TrafficMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionTrafficStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionTrafficStatuses_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegionTrafficStatuses_RegionId",
                table: "RegionTrafficStatuses",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionTrafficStatuses");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
