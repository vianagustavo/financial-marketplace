using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialMarketplace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "initial_value",
                table: "products",
                newName: "minimum_value");

            migrationBuilder.AddColumn<decimal>(
                name: "offer_limit_value",
                table: "products",
                type: "numeric",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "offer_limit_value",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "minimum_value",
                table: "products",
                newName: "initial_value");
        }
    }
}
