using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using clunComp.Models;

namespace clunComp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Computer> Computers { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed данные для компьютеров
        builder.Entity<Computer>().HasData(
            // VIP зона
            new Computer { Id = 1, Name = "VIP-1", Description = "Топовая конфигурация", Specifications = "Intel i9-14900K, RTX 4090, 64GB RAM, 360Hz", PricePerHour = 800, Zone = "VIP", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 2, Name = "VIP-2", Description = "Топовая конфигурация", Specifications = "Intel i9-14900K, RTX 4090, 64GB RAM, 360Hz", PricePerHour = 800, Zone = "VIP", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 3, Name = "VIP-3", Description = "Топовая конфигурация", Specifications = "Intel i9-14900K, RTX 4090, 64GB RAM, 360Hz", PricePerHour = 800, Zone = "VIP", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 4, Name = "VIP-4", Description = "Топовая конфигурация", Specifications = "Intel i9-14900K, RTX 4090, 64GB RAM, 360Hz", PricePerHour = 800, Zone = "VIP", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            
            // Стандарт зона
            new Computer { Id = 5, Name = "Стандарт-1", Description = "Игровой компьютер", Specifications = "Intel i7-13700K, RTX 4070, 32GB RAM, 240Hz", PricePerHour = 400, Zone = "Стандарт", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 6, Name = "Стандарт-2", Description = "Игровой компьютер", Specifications = "Intel i7-13700K, RTX 4070, 32GB RAM, 240Hz", PricePerHour = 400, Zone = "Стандарт", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 7, Name = "Стандарт-3", Description = "Игровой компьютер", Specifications = "Intel i7-13700K, RTX 4070, 32GB RAM, 240Hz", PricePerHour = 400, Zone = "Стандарт", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 8, Name = "Стандарт-4", Description = "Игровой компьютер", Specifications = "Intel i7-13700K, RTX 4070, 32GB RAM, 240Hz", PricePerHour = 400, Zone = "Стандарт", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 9, Name = "Стандарт-5", Description = "Игровой компьютер", Specifications = "Intel i7-13700K, RTX 4070, 32GB RAM, 240Hz", PricePerHour = 400, Zone = "Стандарт", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 10, Name = "Стандарт-6", Description = "Игровой компьютер", Specifications = "Intel i7-13700K, RTX 4070, 32GB RAM, 240Hz", PricePerHour = 400, Zone = "Стандарт", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            
            // PS зона
            new Computer { Id = 11, Name = "PS-1", Description = "PlayStation 5", Specifications = "PS5, 4K TV 65', удобный диван", PricePerHour = 500, Zone = "PS", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 12, Name = "PS-2", Description = "PlayStation 5", Specifications = "PS5, 4K TV 65', удобный диван", PricePerHour = 500, Zone = "PS", ImageUrl = "/images/placeholder.svg", IsAvailable = true },
            new Computer { Id = 13, Name = "PS-3", Description = "PlayStation 5", Specifications = "PS5, 4K TV 65', удобный диван", PricePerHour = 500, Zone = "PS", ImageUrl = "/images/placeholder.svg", IsAvailable = true }
        );
    }
}