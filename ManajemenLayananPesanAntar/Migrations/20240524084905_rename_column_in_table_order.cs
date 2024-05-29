using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class rename_column_in_table_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "processed_by",
                table: "Orders",
                newName: "delivered_by");

            migrationBuilder.RenameColumn(
                name: "completed_at",
                table: "Orders",
                newName: "received_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "received_at",
                table: "Orders",
                newName: "completed_at");

            migrationBuilder.RenameColumn(
                name: "delivered_by",
                table: "Orders",
                newName: "processed_by");
        }
    }
}
