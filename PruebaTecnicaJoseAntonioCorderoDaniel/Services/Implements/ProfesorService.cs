using Microsoft.EntityFrameworkCore;
using PruebaTecnicaJoseAntonioCorderoDaniel.Data;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Implements
{
    public class ProfesorService : IProfesorService
    {
        private readonly ApplicationDbContext _context;

        public ProfesorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CrearProfesorAsync(string nombre, string apellido, string numeroIdentificacion, int escuelaId)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(numeroIdentificacion))
                throw new ArgumentException("Nombre, apellido y número de identificación son obligatorios.");
            if (escuelaId <= 0)
                throw new ArgumentException("ID de escuela inválido.");

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CrearProfesor @p0, @p1, @p2, @p3",
                nombre, apellido, numeroIdentificacion, escuelaId
            );
        }

        public async Task ActualizarProfesorAsync(int profesorId, string nombre, string apellido, string numeroIdentificacion, int escuelaId)
        {
            if (profesorId <= 0 || escuelaId <= 0)
                throw new ArgumentException("ID de profesor o escuela inválido.");
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(numeroIdentificacion))
                throw new ArgumentException("Nombre, apellido y número de identificación son obligatorios.");

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC ActualizarProfesor @p0, @p1, @p2, @p3, @p4",
                profesorId, nombre, apellido, numeroIdentificacion, escuelaId
            );
        }

        public async Task EliminarProfesorAsync(int profesorId)
        {
            if (profesorId <= 0)
                throw new ArgumentException("ID de profesor inválido.");

            await _context.Database.ExecuteSqlRawAsync("EXEC EliminarProfesor @p0", profesorId);
        }

        public async Task<List<ProfesorDTO>> ObtenerProfesoresAsync()
        {
            var profesores = await _context.Profesores
                .FromSqlRaw("EXEC ObtenerProfesores")
                .AsNoTracking()
                .ToListAsync();

            return profesores.Select(p => new ProfesorDTO
            {
                ProfesorID = p.ProfesorID,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                NumeroIdentificacion = p.NumeroIdentificacion,
                EscuelaID = p.EscuelaID
            }).ToList();
        }

        public async Task<ProfesorDTO> ObtenerProfesorPorIdAsync(int profesorId)
        {
            if (profesorId <= 0)
                throw new ArgumentException("ID de profesor inválido.");

            var profesores = await _context.Profesores
                .FromSqlRaw("EXEC ObtenerProfesorPorId @p0", profesorId)
                .AsNoTracking()
                .ToListAsync();

            var profesor = profesores.FirstOrDefault();

            if (profesor == null)
                throw new KeyNotFoundException("No se encontró el profesor con el ID proporcionado.");

            return new ProfesorDTO
            {
                ProfesorID = profesor.ProfesorID,
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                NumeroIdentificacion = profesor.NumeroIdentificacion,
                EscuelaID = profesor.EscuelaID
            };
        }
    }
}
