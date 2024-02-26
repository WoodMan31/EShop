﻿// <auto-generated />
using System;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EShop.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240422203130_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EShop.Core.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EShop.Core.ProductAggregate.ProductBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ProductBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EShop.Core.UserAggregate.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CardNumbers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EShop.Core.ProductAggregate.ComputerMouse", b =>
                {
                    b.HasBaseType("EShop.Core.ProductAggregate.ProductBase");

                    b.Property<string>("ConnectionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Dpi")
                        .HasColumnType("integer");

                    b.Property<string>("SensorType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("ComputerMouse");
                });

            modelBuilder.Entity("EShop.Core.ProductAggregate.Monitor", b =>
                {
                    b.HasBaseType("EShop.Core.ProductAggregate.ProductBase");

                    b.Property<int>("RefreshRate")
                        .HasColumnType("integer");

                    b.Property<int>("ResolutionHeight")
                        .HasColumnType("integer");

                    b.Property<int>("ResolutionWidth")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Monitor");
                });

            modelBuilder.Entity("EShop.Core.OrderAggregate.Order", b =>
                {
                    b.HasOne("EShop.Core.UserAggregate.User", null)
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.OwnsMany("EShop.Core.OrderItem", "Items", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<int>("ProductId")
                                .HasColumnType("integer");

                            b1.Property<int>("Quantity")
                                .HasColumnType("integer");

                            b1.HasKey("OrderId", "Id");

                            b1.HasIndex("ProductId");

                            b1.ToTable("Orders_Items");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.HasOne("EShop.Core.ProductAggregate.ProductBase", "Product")
                                .WithMany()
                                .HasForeignKey("ProductId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("Product");
                        });

                    b.OwnsOne("EShop.Core.PaymentMethod", "PaymentMethod", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("integer");

                            b1.Property<string>("CardNumber")
                                .HasColumnType("text");

                            b1.Property<int>("Type")
                                .HasColumnType("integer");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Items");

                    b.Navigation("PaymentMethod")
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Core.UserAggregate.User", b =>
                {
                    b.OwnsOne("EShop.Core.UserAggregate.ShoppingCart", "ShoppingCart", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.OwnsMany("EShop.Core.OrderItem", "Items", b2 =>
                                {
                                    b2.Property<int>("ShoppingCartUserId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b2.Property<int>("Id"));

                                    b2.Property<int>("ProductId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Quantity")
                                        .HasColumnType("integer");

                                    b2.HasKey("ShoppingCartUserId", "Id");

                                    b2.HasIndex("ProductId");

                                    b2.ToTable("Users_Items");

                                    b2.HasOne("EShop.Core.ProductAggregate.ProductBase", "Product")
                                        .WithMany()
                                        .HasForeignKey("ProductId")
                                        .OnDelete(DeleteBehavior.Cascade)
                                        .IsRequired();

                                    b2.WithOwner()
                                        .HasForeignKey("ShoppingCartUserId");

                                    b2.Navigation("Product");
                                });

                            b1.Navigation("Items");
                        });

                    b.Navigation("ShoppingCart")
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Core.UserAggregate.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
