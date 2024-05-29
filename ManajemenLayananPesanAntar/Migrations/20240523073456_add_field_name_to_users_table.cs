using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Manajemen_Layanan_Pesan_Antar.Migrations
{
    /// <inheritdoc />
    public partial class add_field_name_to_users_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Users");
        }
    }
}
