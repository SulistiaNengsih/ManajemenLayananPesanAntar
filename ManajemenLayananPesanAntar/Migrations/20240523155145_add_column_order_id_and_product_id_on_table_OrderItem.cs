using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class add_column_order_id_and_product_id_on_table_OrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_Orderid",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_productid",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_Orderid",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "OrderItem",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_productid",
                table: "OrderItem",
                newName: "IX_OrderItem_product_id");

            migrationBuilder.AddColumn<long>(
                name: "order_id",
                table: "OrderItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_order_id",
                table: "OrderItem",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_order_id",
                table: "OrderItem",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_product_id",
                table: "OrderItem",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_order_id",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_product_id",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_order_id",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "order_id",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "OrderItem",
                newName: "productid");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_product_id",
                table: "OrderItem",
                newName: "IX_OrderItem_productid");

            migrationBuilder.AddColumn<long>(
                name: "Orderid",
                table: "OrderItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_Orderid",
                table: "OrderItem",
                column: "Orderid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_Orderid",
                table: "OrderItem",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_productid",
                table: "OrderItem",
                column: "productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
