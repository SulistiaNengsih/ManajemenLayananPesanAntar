using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class add_column_unit_price_in_products_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "unit_price",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unit_price",
                table: "Products");
        }
    }
}
