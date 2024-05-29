using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class add_table_order_delivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDelivery_Orders_order_id",
                table: "OrderDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_order_id",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_product_id",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDelivery",
                table: "OrderDelivery");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "Order_Item");

            migrationBuilder.RenameTable(
                name: "OrderDelivery",
                newName: "Order_Deliveries");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_product_id",
                table: "Order_Item",
                newName: "IX_Order_Item_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_order_id",
                table: "Order_Item",
                newName: "IX_Order_Item_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDelivery_order_id",
                table: "Order_Deliveries",
                newName: "IX_Order_Deliveries_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Item",
                table: "Order_Item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Deliveries",
                table: "Order_Deliveries",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Deliveries_Orders_order_id",
                table: "Order_Deliveries",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Deliveries_Orders_order_id",
                table: "Order_Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Item_Orders_order_id",
                table: "Order_Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Item_Products_product_id",
                table: "Order_Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Item",
                table: "Order_Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Deliveries",
                table: "Order_Deliveries");

            migrationBuilder.RenameTable(
                name: "Order_Item",
                newName: "OrderItem");

            migrationBuilder.RenameTable(
                name: "Order_Deliveries",
                newName: "OrderDelivery");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Item_product_id",
                table: "OrderItem",
                newName: "IX_OrderItem_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Item_order_id",
                table: "OrderItem",
                newName: "IX_OrderItem_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Deliveries_order_id",
                table: "OrderDelivery",
                newName: "IX_OrderDelivery_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDelivery",
                table: "OrderDelivery",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDelivery_Orders_order_id",
                table: "OrderDelivery",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
