using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaJoseAntonioCorderoDaniel.Data;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Implements
{
    public class AlumnoService : IAlumnoService
    {
        private readonly ApplicationDbContext _context;

        public AlumnoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CrearAlumnoAsync(string nombre, string apellido, DateTime fechaNacimiento, string numeroIdentificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@Nombre", nombre),
                new SqlParameter("@Apellido", apellido),
                new SqlParameter("@FechaNacimiento", fechaNacimiento),
                new SqlParameter("@NumeroIdentificacion", numeroIdentificacion)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC CrearAlumno @Nombre, @Apellido, @FechaNacimiento, @NumeroIdentificacion", parameters);
        }

        public async Task ActualizarAlumnoAsync(int alumnoId, string nombre, string apellido, DateTime fechaNacimiento, string numeroIdentificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@AlumnoID", alumnoId),
                new SqlParameter("@Nombre", nombre),
                new SqlParameter("@Apellido", apellido),
                new SqlParameter("@FechaNacimiento", fechaNacimiento),
                new SqlParameter("@NumeroIdentificacion", numeroIdentificacion)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC ActualizarAlumno @AlumnoID, @Nombre, @Apellido, @FechaNacimiento, @NumeroIdentificacion", parameters);
        }

        public async Task EliminarAlumnoAsync(int alumnoId)
        {
            var parameter = new SqlParameter("@AlumnoID", alumnoId);

            await _context.Database.ExecuteSqlRawAsync("EXEC EliminarAlumno @AlumnoID", parameter);
        }

        public async Task<List<AlumnoDTO>> ObtenerAlumnosAsync()
        {
            var alumnos = await _context.Alumnos
                .FromSqlRaw("EXEC ObtenerAlumnos")
                .AsNoTracking() // opcional pero recomendable
                .ToListAsync(); // ✅ no se encadena nada después

            return alumnos.Select(a => new AlumnoDTO
            {
                AlumnoID = a.AlumnoID,
                Nombre = a.Nombre,
                Apellido = a.Apellido,
                FechaNacimiento = a.FechaNacimiento,
                NumeroIdentificacion = a.NumeroIdentificacion
            }).ToList();
        }

        public async Task<AlumnoDTO> ObtenerAlumnoPorIdAsync(int alumnoId)
        {
            var param = new SqlParameter("@AlumnoID", alumnoId);

            // Ejecutar el SP sin composición y traer directamente la entidad
            var alumnoEntity = await _context.Alumnos
                .FromSqlRaw("EXEC ObtenerAlumnoPorId @AlumnoID", param)
                .AsNoTracking() // opcional pero recomendado
                .ToListAsync(); // <-- importante: aquí ya no usamos .SingleOrDefaultAsync()

            var alumno = alumnoEntity.FirstOrDefault(); // hacer esto en memoria, NO en SQL

            if (alumno == null)
                throw new KeyNotFoundException("Alumno no encontrado.");

            // Mapear manualmente
            return new AlumnoDTO
            {
                AlumnoID = alumno.AlumnoID,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                FechaNacimiento = alumno.FechaNacimiento,
                NumeroIdentificacion = alumno.NumeroIdentificacion
            };
        }
    }
}
