using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingNotificationDb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    BoxId = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.BoxId);
                });

            migrationBuilder.CreateTable(
                name: "ProductLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PoNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Isbn = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    BoxId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLine_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "BoxId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_BoxId",
                table: "ProductLine",
                column: "BoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLine");

            migrationBuilder.DropTable(
                name: "Boxes");
        }
    }
}
