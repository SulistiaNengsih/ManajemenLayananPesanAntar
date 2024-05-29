using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class remove_status_remark_add_suspend_remark_and_cancel_remark_in_order_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status_remark",
                table: "Orders",
                newName: "suspend_remark");

            migrationBuilder.AddColumn<string>(
                name: "cancel_remark",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "canceled_at",
                table: "Orders",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cancel_remark",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "canceled_at",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "suspend_remark",
                table: "Orders",
                newName: "status_remark");
        }
    }
}
