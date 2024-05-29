using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class rename_table_order_item_to_order_items : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Item_Orders_order_id",
                table: "Order_Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Item_Products_product_id",
                table: "Order_Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Item",
                table: "Order_Item");

            migrationBuilder.RenameTable(
                name: "Order_Item",
                newName: "Order_Items");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Item_product_id",
                table: "Order_Items",
                newName: "IX_Order_Items_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Item_order_id",
                table: "Order_Items",
                newName: "IX_Order_Items_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Items",
                table: "Order_Items",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Items_Orders_order_id",
                table: "Order_Items",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Items_Products_product_id",
                table: "Order_Items",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Items_Orders_order_id",
                table: "Order_Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Items_Products_product_id",
                table: "Order_Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Items",
                table: "Order_Items");

            migrationBuilder.RenameTable(
                name: "Order_Items",
                newName: "Order_Item");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Items_product_id",
                table: "Order_Item",
                newName: "IX_Order_Item_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Items_order_id",
                table: "Order_Item",
                newName: "IX_Order_Item_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Item",
                table: "Order_Item",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Item_Orders_order_id",
                table: "Order_Item",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Item_Products_product_id",
                table: "Order_Item",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
