﻿// <auto-generated />
using System;
using GeekShopping.OrderAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.OrderAPI.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20230429135818_AddOrderDataTablesOnDb")]
    partial class AddOrderDataTablesOnDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShopping.OrderAPI.Model.OrderDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("count");

                    b.Property<long>("OrderHeaderId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("price");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("ProductId");

                    b.Property<string>("ProductName")
                        .HasColumnType("longtext")
                        .HasColumnName("product_name");

                    b.HasKey("Id");

                    b.HasIndex("OrderHeaderId");

                    b.ToTable("order_detail");
                });

            modelBuilder.Entity("GeekShopping.OrderAPI.Model.OrderHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CVV")
                        .HasColumnType("longtext")
                        .HasColumnName("cvv");

                    b.Property<string>("CardNumber")
                        .HasColumnType("longtext")
                        .HasColumnName("card_number");

                    b.Property<int>("CartTotalItens")
                        .HasColumnType("int")
                        .HasColumnName("total_itens");

                    b.Property<string>("CouponCode")
                        .HasColumnType("longtext")
                        .HasColumnName("coupon_code");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("purchase_date");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("discount_amount");

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("ExpireMonthYear")
                        .HasColumnType("longtext")
                        .HasColumnName("expiry_month_year");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("order_time");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("payment_status");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext")
                        .HasColumnName("phone_number");

                    b.Property<decimal>("PurchaseAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("purchase_amount");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("order_header");
                });

            modelBuilder.Entity("GeekShopping.OrderAPI.Model.OrderDetail", b =>
                {
                    b.HasOne("GeekShopping.OrderAPI.Model.OrderHeader", "OrderHeader")
                        .WithMany("CartDetails")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("GeekShopping.OrderAPI.Model.OrderHeader", b =>
                {
                    b.Navigation("CartDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
