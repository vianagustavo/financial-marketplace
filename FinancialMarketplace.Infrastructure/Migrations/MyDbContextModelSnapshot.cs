﻿using FinancialMarketplace.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace FinancialMarketplace.Infrastructure.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccountProduct", b =>
                {
                    b.Property<Guid>("AccountsId")
                        .HasColumnType("uuid")
                        .HasColumnName("accounts_id");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uuid")
                        .HasColumnName("products_id");

                    b.HasKey("AccountsId", "ProductsId")
                        .HasName("pk_account_product");

                    b.HasIndex("AccountsId")
                        .HasDatabaseName("ix_account_product_accounts_id");

                    b.ToTable("account_product", (string)null);
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric")
                        .HasColumnName("balance");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("branch");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_accounts");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_accounts_user_id");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<decimal>("InitialValue")
                        .HasColumnType("numeric")
                        .HasColumnName("initial_value");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<decimal>("MarketValue")
                        .HasColumnType("numeric")
                        .HasColumnName("market_value");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("permissions");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_users_role_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expires_at");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_used");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_tokens");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_tokens_user_id");

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("AccountProduct", b =>
                {
                    b.HasOne("FinancialMarketplace.Domain.Users.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_account_product_accounts_accounts_id");

                    b.HasOne("FinancialMarketplace.Domain.Users.Product", null)
                        .WithMany()
                        .HasForeignKey("AccountsId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_account_product_products_products_id");
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.Account", b =>
                {
                    b.HasOne("FinancialMarketplace.Domain.Users.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("FinancialMarketplace.Domain.Users.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_accounts_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.User", b =>
                {
                    b.HasOne("FinancialMarketplace.Domain.Users.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_role_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.UserToken", b =>
                {
                    b.HasOne("FinancialMarketplace.Domain.Users.User", null)
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_tokens_users_user_id");
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("FinancialMarketplace.Domain.Users.User", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
