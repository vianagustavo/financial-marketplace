using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialMarketplace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductDueAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "due_at",
                table: "products",
                type: "timestamp with time zone",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "due_at",
                table: "products");
        }
    }
}
