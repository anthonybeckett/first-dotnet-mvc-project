using Microsoft.EntityFrameworkCore;

namespace FirstProject.Local.Data;

public class ProductContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public string DbPath { get; set; }

    public ProductContext(IConfiguration config)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath = Path.Join(path, config.GetConnectionString("ProductDbFilename"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public void SeedInitialData()
    {
        if(Products.Any()){
            Products.RemoveRange(Products);
            SaveChanges();
        }

        if (Categories.Any())
        {
            Categories.RemoveRange(Categories);
            SaveChanges();
        }

        var categoryOne = new Category { Id = 1, Name = "Category One" };
        var categoryTwo = new Category { Id = 2, Name = "Category Two" };

        Products.Add(new Product {
            Id = 1,
            Name = "product 1",
            Description = "A test description",
            Price = 10.00M,
            IsActive = true,
            Category = categoryOne
        });

        Products.Add(new Product {
            Id = 2,
            Name = "product 2",
            Description = "A test description",
            Price = 20.00M,
            IsActive = true,
            Category = categoryOne
        });

        Products.Add(new Product {
            Id = 3,
            Name = "product 3",
            Description = "A test description",
            Price = 30.00M,
            IsActive = true,
            Category = categoryTwo
        });

        Categories.Add(categoryOne);
        Categories.Add(categoryTwo);

        SaveChanges();
    }
}