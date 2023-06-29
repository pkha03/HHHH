using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTH626.Migrations
{
    /// <inheritdoc />
    public partial class Lophoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lophoc",
                columns: table => new
                {
                    tenlop = table.Column<string>(type: "TEXT", nullable: false),
                    malop = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lophoc", x => x.tenlop);
                });

            migrationBuilder.CreateTable(
                name: "Sinhvien",
                columns: table => new
                {
                    masinhvien = table.Column<string>(type: "TEXT", nullable: false),
                    tensinhvien = table.Column<string>(type: "TEXT", nullable: false),
                    malop = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinhvien", x => x.masinhvien);
                    table.ForeignKey(
                        name: "FK_Sinhvien_Lophoc_malop",
                        column: x => x.malop,
                        principalTable: "Lophoc",
                        principalColumn: "tenlop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sinhvien_malop",
                table: "Sinhvien",
                column: "malop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sinhvien");

            migrationBuilder.DropTable(
                name: "Lophoc");
        }
    }
}
