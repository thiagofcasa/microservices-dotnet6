using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }


        //metodo para inserir uma carga de dados...
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Binary Tshirt",
                Price = new decimal(69.9),
                Description = "Binary Geek Tshirt",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "T-Shirt"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Funko MiniYoda",
                Price = new decimal(15.94),
                Description = "Funko pop mini yoda",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "T-Shirt"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Boneco Darth Vader",
                Price = new decimal(16.99),
                Description = "Boneco miniatura Dart Vader",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "Boneco"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "Caneca Yoda",
                Price = new decimal(30.9),
                Description = "Caneca Yoda Verdinho",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "Caneca"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 6,
                Name = "Capacete guerreiro",
                Price = new decimal(200.4),
                Description = "Capacete de guerra intergalactica",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "Capacete"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 7,
                Name = "Star Wars Saga",
                Price = new decimal(350.9),
                Description = "Saga Star Wars em Blueray",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "Blueray"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 8,
                Name = "Biscoito Cheubacca",
                Price = new decimal(8.9),
                Description = "Pacote de biscoito Cheubacca 180g",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "Biscoito"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 9,
                Name = "Lango Lango Alien",
                Price = new decimal(1257.5),
                Description = "Lango lango alienigena camaleão",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "Brinquedo"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 10,
                Name = "Mini pelucia Raul Seixas",
                Price = new decimal(69),
                Description = "Mini pelucia do Raul Seixas com oculos escuro.",
                ImageUrl = "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C71btpBk-iqL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UL1500_.png",
                CategoryName = "Pelucia"
            });


        }
    }
}
