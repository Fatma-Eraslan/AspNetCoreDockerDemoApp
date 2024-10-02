using AspNetCoreDockerDemoApp.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AspNetCoreDockerDemoApp.Data;

public class OnlineShopDbContext : DbContext
{
    public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options)
    : base(options)
    {
        var dbCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        if (dbCreater != null)
        {
            // database oluştur
            if (!dbCreater.CanConnect())
            {
                dbCreater.Create();
            }

            // tablo oluştur
            if (!dbCreater.HasTables())
            {
                dbCreater.CreateTables();
            }
        }
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product() { Id = 1, Name = "Apple iPad", Price = 1000 },
            new Product() { Id = 2, Name = "Samsung TV", Price = 1500 },
            new Product() { Id = 3, Name = "Nokia 3310", Price = 1200 });
    }
}
