using APIKEY.Crudes.Models;
using Microsoft.EntityFrameworkCore;

namespace APIKEY.Crudes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Carro> Carros { get; set; }
    }
}
