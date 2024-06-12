using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class change_order_delivery_latitude_and_longitude_data_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "delivery_longitude",
                table: "Order_Deliveries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "delivery_latitude",
                table: "Order_Deliveries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "courier_longitude",
                table: "Order_Deliveries",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "courier_latitude",
                table: "Order_Deliveries",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "delivery_longitude",
                table: "Order_Deliveries",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "delivery_latitude",
                table: "Order_Deliveries",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "courier_longitude",
                table: "Order_Deliveries",
                type: "decimal(9,6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "courier_latitude",
                table: "Order_Deliveries",
                type: "decimal(8,6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
