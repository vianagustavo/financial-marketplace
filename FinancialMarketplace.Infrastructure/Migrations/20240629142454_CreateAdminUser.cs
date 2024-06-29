using FinancialMarketplace.Application.Exceptions;
using FinancialMarketplace.Infrastructure.External;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialMarketplace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminRoleId = Environment.GetEnvironmentVariable("ADMIN_ROLE_ID") ?? throw new MissingEnvironmentVariableException("ADMIN_ROLE_ID");
            var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? throw new MissingEnvironmentVariableException("ADMIN_EMAIL");
            var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? throw new MissingEnvironmentVariableException("ADMIN_PASSWORD");

            var _cryptoHandler = new CryptoHandler();
            string hashedPassword = _cryptoHandler.Encrypt(adminPassword);


            migrationBuilder.Sql($@"
                INSERT INTO users (id, name, email, password, is_active, created_at, role_id)
                VALUES (gen_random_uuid(), 'Admin', '{adminEmail}', '{hashedPassword}', true, NOW(), '{adminRoleId}');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? throw new MissingEnvironmentVariableException("ADMIN_EMAIL");

            migrationBuilder.Sql($@"
                DELETE FROM Users
                WHERE Email = '{adminEmail}';
            ");
        }
    }
}
