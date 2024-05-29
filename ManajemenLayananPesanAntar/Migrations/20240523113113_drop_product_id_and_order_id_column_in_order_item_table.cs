using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class drop_product_id_and_order_id_column_in_order_item_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_id",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "OrderItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "order_id",
                table: "OrderItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "product_id",
                table: "OrderItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
