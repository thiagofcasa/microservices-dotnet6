using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.ProductAPI.Migrations
{
    public partial class SeedProductDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[,]
                {
                    { 2L, "T-Shirt", "Binary Geek Tshirt", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Binary Tshirt", 69.9m },
                    { 3L, "T-Shirt", "Funko pop mini yoda", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Funko MiniYoda", 15.94m },
                    { 4L, "Boneco", "Boneco miniatura Dart Vader", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Boneco Darth Vader", 16.99m },
                    { 5L, "Caneca", "Caneca Yoda Verdinho", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Caneca Yoda", 30.9m },
                    { 6L, "Capacete", "Capacete de guerra intergalactica", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Capacete guerreiro", 200.4m },
                    { 7L, "Blueray", "Saga Star Wars em Blueray", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Star Wars Saga", 350.9m },
                    { 8L, "Biscoito", "Pacote de biscoito Cheubacca 180g", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Biscoito Cheubacca", 8.9m },
                    { 9L, "Brinquedo", "Lango lango alienigena camaleão", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Lango Lango Alien", 1257.5m },
                    { 10L, "Pelucia", "Mini pelucia do Raul Seixas com oculos escuro.", "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png", "Mini pelucia Raul Seixas", 69m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 10L);
        }
    }
}
