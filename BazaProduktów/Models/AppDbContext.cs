using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BazaProduktow.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produkt> Produkty { get; set; }
    }
}