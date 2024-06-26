using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialMarketplace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminRoleId = Environment.GetEnvironmentVariable("ADMIN_ROLE_ID");

            if (string.IsNullOrEmpty(adminRoleId))
            {
                throw new InvalidOperationException("Variável de ambiente ADMIN_ROLE_ID não foi definida");
            }

            migrationBuilder.Sql($@"
                INSERT INTO Roles (id, name, permissions, created_at, updated_at)
                VALUES (
                    '{adminRoleId}', 
                    'Admin', 
                    'ManageProducts,ManageUsers,NegotiateProducts,ManageAccount',
                    NOW(), 
                    NULL
                )");

            var operationalRoleId = Environment.GetEnvironmentVariable("OPERATIONAL_ROLE_ID");

            if (string.IsNullOrEmpty(operationalRoleId))
            {
                throw new InvalidOperationException("Variável de ambiente OPERATIONAL_ROLE_ID não foi definida");
            }

            migrationBuilder.Sql($@"
                INSERT INTO Roles (id, name, permissions, created_at, updated_at)
                VALUES (
                    '{operationalRoleId}', 
                    'Operational', 
                    'ManageProducts,ManageUsers',
                    NOW(), 
                    NULL
                )");

            var clientRoleId = Environment.GetEnvironmentVariable("CLIENT_ROLE_ID");

            if (string.IsNullOrEmpty(clientRoleId))
            {
                throw new InvalidOperationException("Variável de ambiente CLIENT_ROLE_ID não foi definida");
            }

            migrationBuilder.Sql($@"
                INSERT INTO Roles (id, name, permissions, created_at, updated_at)
                VALUES (
                    '{clientRoleId}', 
                    'Client', 
                    'NegotiateProducts,ManageAccount',
                    NOW(), 
                    NULL
                )");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Roles WHERE Name IN ('Admin', 'Operational', 'Client')");
        }
    }
}
