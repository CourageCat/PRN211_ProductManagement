﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SWP.ProductManagement.Repository;

#nullable disable

namespace SWP.ProductManagement.Repository.Migrations
{
    [DbContext(typeof(ProductManagementContext))]
    partial class ProductManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SWP.ProductManagement.Repository.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Beverages"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Condiments"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Confections"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Dairy Products"
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryName = "Grains/Cereals"
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryName = "Meat/Poultry"
                        },
                        new
                        {
                            CategoryId = 7,
                            CategoryName = "Produce"
                        },
                        new
                        {
                            CategoryId = 8,
                            CategoryName = "Seafood"
                        });
                });

            modelBuilder.Entity("SWP.ProductManagement.Repository.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("UnitsInStock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Products_CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            ProductName = "Chai",
                            UnitPrice = 18.00m,
                            UnitsInStock = 39
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            ProductName = "Chang",
                            UnitPrice = 19.00m,
                            UnitsInStock = 17
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            ProductName = "Aniseed Syrup",
                            UnitPrice = 10.00m,
                            UnitsInStock = 13
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2,
                            ProductName = "Chef Anton's Cajun Seasoning",
                            UnitPrice = 22.00m,
                            UnitsInStock = 53
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 2,
                            ProductName = "Chef Anton's Gumbo Mix",
                            UnitPrice = 21.35m,
                            UnitsInStock = 0
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 2,
                            ProductName = "Grandma's Boysenberry Spread",
                            UnitPrice = 25.00m,
                            UnitsInStock = 120
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 7,
                            ProductName = "Uncle Bob's Organic Dried Pears",
                            UnitPrice = 30.00m,
                            UnitsInStock = 15
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 2,
                            ProductName = "Northwoods Cranberry Sauce",
                            UnitPrice = 40.00m,
                            UnitsInStock = 6
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 6,
                            ProductName = "Mishi Kobe Niku",
                            UnitPrice = 97.00m,
                            UnitsInStock = 29
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 8,
                            ProductName = "Ikura",
                            UnitPrice = 31.00m,
                            UnitsInStock = 31
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryId = 4,
                            ProductName = "Queso Cabrales",
                            UnitPrice = 21.00m,
                            UnitsInStock = 22
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 4,
                            ProductName = "Queso Manchego La Pastora",
                            UnitPrice = 38.00m,
                            UnitsInStock = 86
                        },
                        new
                        {
                            ProductId = 13,
                            CategoryId = 8,
                            ProductName = "Konbu",
                            UnitPrice = 6.00m,
                            UnitsInStock = 24
                        },
                        new
                        {
                            ProductId = 14,
                            CategoryId = 7,
                            ProductName = "Tofu",
                            UnitPrice = 23.25m,
                            UnitsInStock = 35
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 2,
                            ProductName = "Genen Shouyu",
                            UnitPrice = 15.50m,
                            UnitsInStock = 39
                        },
                        new
                        {
                            ProductId = 16,
                            CategoryId = 3,
                            ProductName = "Pavlova",
                            UnitPrice = 17.45m,
                            UnitsInStock = 52
                        },
                        new
                        {
                            ProductId = 17,
                            CategoryId = 6,
                            ProductName = "Alice Mutton",
                            UnitPrice = 39.00m,
                            UnitsInStock = 0
                        },
                        new
                        {
                            ProductId = 18,
                            CategoryId = 8,
                            ProductName = "Carnarvon Tigers",
                            UnitPrice = 62.50m,
                            UnitsInStock = 8
                        },
                        new
                        {
                            ProductId = 19,
                            CategoryId = 3,
                            ProductName = "Teatime Chocolate Biscuits",
                            UnitPrice = 9.20m,
                            UnitsInStock = 25
                        },
                        new
                        {
                            ProductId = 20,
                            CategoryId = 3,
                            ProductName = "Sir Rodney's Marmalade",
                            UnitPrice = 81.00m,
                            UnitsInStock = 40
                        },
                        new
                        {
                            ProductId = 21,
                            CategoryId = 3,
                            ProductName = "Sir Rodney's Scones",
                            UnitPrice = 10.00m,
                            UnitsInStock = 3
                        });
                });

            modelBuilder.Entity("SWP.ProductManagement.Repository.Models.Product", b =>
                {
                    b.HasOne("SWP.ProductManagement.Repository.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SWP.ProductManagement.Repository.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
