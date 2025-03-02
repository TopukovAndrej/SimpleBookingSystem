using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Uid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "NVARCHAR", maxLength: 30, nullable: false),
                    TotalQuantity = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Uid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    BookedQuantity = table.Column<int>(type: "INT", nullable: false),
                    FromDate = table.Column<DateTime>(type: "DATETIME2(0)", nullable: false),
                    ToDate = table.Column<DateTime>(type: "DATETIME2(0)", nullable: false),
                    ResourceFk = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Resources_ResourceFk",
                        column: x => x.ResourceFk,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ResourceFk",
                table: "Bookings",
                column: "ResourceFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
