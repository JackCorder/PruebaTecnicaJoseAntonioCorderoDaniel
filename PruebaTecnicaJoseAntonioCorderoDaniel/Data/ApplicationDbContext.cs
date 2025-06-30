using Microsoft.EntityFrameworkCore;
using PruebaTecnicaJoseAntonioCorderoDaniel.Models;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Escuela> Escuelas { get; set; }

    }
}
