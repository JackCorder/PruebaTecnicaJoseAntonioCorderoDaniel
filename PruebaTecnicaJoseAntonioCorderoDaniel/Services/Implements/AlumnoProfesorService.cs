using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaJoseAntonioCorderoDaniel.Data;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Implements
{
    public class AlumnoProfesorService : IAlumnoProfesorService
    {
        private readonly ApplicationDbContext _context;

        public AlumnoProfesorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AsignarProfesorAAlumnoAsync(int alumnoId, int profesorId)
        {
            try
            {
                var parameters = new[]
                {
                new SqlParameter("@AlumnoID", alumnoId),
                new SqlParameter("@ProfesorID", profesorId)
            };

                await _context.Database.ExecuteSqlRawAsync("EXEC AsignarProfesorAAlumno @AlumnoID, @ProfesorID", parameters);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al asignar profesor al alumno: {ex.Message}", ex);
            }
        }

        public async Task EliminarAsignacionAsync(int alumnoId, int profesorId)
        {
            try
            {
                var parameters = new[]
                {
                new SqlParameter("@AlumnoID", alumnoId),
                new SqlParameter("@ProfesorID", profesorId)
            };

                await _context.Database.ExecuteSqlRawAsync("EXEC EliminarAsignacionAlumnoProfesor @AlumnoID, @ProfesorID", parameters);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al eliminar la asignación: {ex.Message}", ex);
            }
        }

        public async Task<List<ProfesorRelacionDTO>> ObtenerProfesoresDeAlumnoAsync(int alumnoId)
        {
            try
            {
                var param = new SqlParameter("@AlumnoID", alumnoId);

                return await _context.ProfesoresRelacion
                    .FromSqlRaw("EXEC ObtenerProfesoresDeAlumno @AlumnoID", param)
                    .ToListAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al obtener profesores del alumno: {ex.Message}", ex);
            }
        }

        public async Task<List<AlumnoRelacionDTO>> ObtenerAlumnosDeProfesorAsync(int profesorId)
        {
            try
            {
                var param = new SqlParameter("@ProfesorID", profesorId);

                return await _context.AlumnosRelacion
                    .FromSqlRaw("EXEC ObtenerAlumnosDeProfesor @ProfesorID", param)
                    .ToListAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al obtener alumnos del profesor: {ex.Message}", ex);
            }
        }

        public async Task<List<AlumnoConEscuelaDTO>> ObtenerAlumnosInscritosPorProfesorAsync(int profesorId)
        {
            try
            {
                var param = new SqlParameter("@ProfesorID", profesorId);

                var resultado = await _context.Set<AlumnoConEscuelaDTO>()
                    .FromSqlRaw("EXEC ObtenerAlumnosInscritosPorProfesor @ProfesorID", param)
                    .AsNoTracking()
                    .ToListAsync();

                return resultado;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al obtener alumnos inscritos por profesor: {ex.Message}", ex);
            }
        }

        public async Task<List<EscuelaConAlumnosDTO>> ObtenerEscuelasYAlumnosDeProfesorAsync(int profesorId)
        {
            try
            {
                var param = new SqlParameter("@ProfesorID", profesorId);

                var flatData = await _context.Set<EscuelaAlumnoDTO>()
                    .FromSqlRaw("EXEC ObtenerEscuelasYAlumnosDeProfesor @ProfesorID", param)
                    .AsNoTracking()
                    .ToListAsync();

                var resultado = flatData
                    .GroupBy(e => e.NombreEscuela)
                    .Select(g => new EscuelaConAlumnosDTO
                    {
                        NombreEscuela = g.Key,
                        Alumnos = g.Select(a => new AlumnoSimpleDTO
                        {
                            AlumnoID = a.AlumnoID,
                            Nombre = a.NombreAlumno,
                            Apellido = a.ApellidoAlumno,
                            NumeroIdentificacion = a.NumeroIdentificacion
                        }).ToList()
                    }).ToList();

                return resultado;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al obtener escuelas y alumnos del profesor: {ex.Message}", ex);
            }
        }


    }
}
