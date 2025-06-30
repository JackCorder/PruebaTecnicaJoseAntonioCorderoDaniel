using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaJoseAntonioCorderoDaniel.Data;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;
using System.Data;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Implements
{
    public class InscripcionService : IInscripcionService
    {
        private readonly ApplicationDbContext _context;

        public InscripcionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InscripcionDTO>> ObtenerInscripcionesAsync()
        {
            var resultado = new List<InscripcionDTO>();

            var connection = _context.Database.GetDbConnection();
            await using var command = connection.CreateCommand();
            command.CommandText = "ObtenerInscripciones";
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                resultado.Add(new InscripcionDTO
                {
                    InscripcionID = reader.GetInt32(0),
                    FechaInscripcion = reader.GetDateTime(1),
                    AlumnoID = reader.GetInt32(2),
                    NombreAlumno = reader.GetString(3),
                    ApellidoAlumno = reader.GetString(4),
                    EscuelaID = reader.GetInt32(5),
                    NombreEscuela = reader.GetString(6),
                });
            }

            return resultado;
        }

        public async Task<InscripcionDTO> ObtenerInscripcionPorIdAsync(int inscripcionId)
        {
            var connection = _context.Database.GetDbConnection();
            await using var command = connection.CreateCommand();
            command.CommandText = "ObtenerInscripcionPorId";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@InscripcionID", inscripcionId));

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new InscripcionDTO
                {
                    InscripcionID = reader.GetInt32(0),
                    FechaInscripcion = reader.GetDateTime(1),
                    AlumnoID = reader.GetInt32(2),
                    NombreAlumno = reader.GetString(3),
                    ApellidoAlumno = reader.GetString(4),
                    EscuelaID = reader.GetInt32(5),
                    NombreEscuela = reader.GetString(6),
                };
            }

            throw new KeyNotFoundException("Inscripción no encontrada.");
        }

        public async Task CrearInscripcionAsync(InscripcionCreateDTO dto)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC CrearInscripcion @AlumnoID, @EscuelaID",
                new SqlParameter("@AlumnoID", dto.AlumnoID),
                new SqlParameter("@EscuelaID", dto.EscuelaID));
        }

        public async Task ActualizarInscripcionAsync(int inscripcionId, InscripcionCreateDTO dto)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC ActualizarInscripcion @InscripcionID, @AlumnoID, @EscuelaID",
                new SqlParameter("@InscripcionID", inscripcionId),
                new SqlParameter("@AlumnoID", dto.AlumnoID),
                new SqlParameter("@EscuelaID", dto.EscuelaID));
        }

        public async Task EliminarInscripcionAsync(int inscripcionId)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC EliminarInscripcion @InscripcionID",
                new SqlParameter("@InscripcionID", inscripcionId));
        }
    }

}
