using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaJoseAntonioCorderoDaniel.Data;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;
using System.Data;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Implements
{
    public class EscuelaService : IEscuelaService
    {
        private readonly ApplicationDbContext _context;

        public EscuelaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CrearEscuelaAsync(string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CrearEscuela @p0, @p1", nombre, descripcion ?? string.Empty
            );
        }

        public async Task ActualizarEscuelaAsync(int id, string nombre, string descripcion)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC ActualizarEscuela @p0, @p1, @p2",
                id, nombre, descripcion ?? string.Empty
            );
        }

        public async Task EliminarEscuelaAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            await _context.Database.ExecuteSqlRawAsync("EXEC EliminarEscuela @p0", id);
        }
        public async Task<List<EscuelaDTO>> ObtenerEscuelasAsync()
        {
            var escuelas = await _context.Escuelas
                .FromSqlRaw("EXEC ObtenerEscuelas")
                .AsNoTracking()
                .ToListAsync();

            return escuelas.Select(e => new EscuelaDTO
            {
                EscuelaID = e.EscuelaID,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Codigo = e.Codigo
            }).ToList();
        }

        public async Task<EscuelaDTO> ObtenerEscuelaPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            // Ejecuta el procedimiento y convierte a lista para que el resto sea en memoria
            var escuelas = await _context.Escuelas
                .FromSqlRaw("EXEC ObtenerEscuelaPorId @p0", id)
                .AsNoTracking()
                .ToListAsync();

            var escuelaEntidad = escuelas.FirstOrDefault();

            if (escuelaEntidad == null)
                throw new KeyNotFoundException("No se encontró la escuela con el ID proporcionado.");

            return new EscuelaDTO
            {
                EscuelaID = escuelaEntidad.EscuelaID,
                Nombre = escuelaEntidad.Nombre,
                Descripcion = escuelaEntidad.Descripcion,
                Codigo = escuelaEntidad.Codigo
            };
        }

    }
}
