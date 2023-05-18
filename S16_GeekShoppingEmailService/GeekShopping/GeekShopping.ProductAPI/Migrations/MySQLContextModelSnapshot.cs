﻿// <auto-generated />
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.ProductAPI.Migrations
{
    [DbContext(typeof(MySQLContext))]
    partial class MySQLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShopping.ProductAPI.Model.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("category_name");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.ToTable("product");

                    b.HasData(
                        new
                        {
                            Id = 2L,
                            CategoryName = "T-Shirt",
                            Description = "Binary Geek Tshirt",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Binary Tshirt",
                            Price = 69.9m
                        },
                        new
                        {
                            Id = 3L,
                            CategoryName = "T-Shirt",
                            Description = "Funko pop mini yoda",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Funko MiniYoda",
                            Price = 15.94m
                        },
                        new
                        {
                            Id = 4L,
                            CategoryName = "Boneco",
                            Description = "Boneco miniatura Dart Vader",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Boneco Darth Vader",
                            Price = 16.99m
                        },
                        new
                        {
                            Id = 5L,
                            CategoryName = "Caneca",
                            Description = "Caneca Yoda Verdinho",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Caneca Yoda",
                            Price = 30.9m
                        },
                        new
                        {
                            Id = 6L,
                            CategoryName = "Capacete",
                            Description = "Capacete de guerra intergalactica",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Capacete guerreiro",
                            Price = 200.4m
                        },
                        new
                        {
                            Id = 7L,
                            CategoryName = "Blueray",
                            Description = "Saga Star Wars em Blueray",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Star Wars Saga",
                            Price = 350.9m
                        },
                        new
                        {
                            Id = 8L,
                            CategoryName = "Biscoito",
                            Description = "Pacote de biscoito Cheubacca 180g",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Biscoito Cheubacca",
                            Price = 8.9m
                        },
                        new
                        {
                            Id = 9L,
                            CategoryName = "Brinquedo",
                            Description = "Lango lango alienigena camaleão",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Lango Lango Alien",
                            Price = 1257.5m
                        },
                        new
                        {
                            Id = 10L,
                            CategoryName = "Pelucia",
                            Description = "Mini pelucia do Raul Seixas com oculos escuro.",
                            ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                            Name = "Mini pelucia Raul Seixas",
                            Price = 69m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
