using Microsoft.EntityFrameworkCore;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Models;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<ProfesorRelacionDTO> ProfesoresRelacion { get; set; }
        public DbSet<AlumnoRelacionDTO> AlumnosRelacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProfesorRelacionDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<AlumnoRelacionDTO>().HasNoKey().ToView(null);
        }

    }
}
