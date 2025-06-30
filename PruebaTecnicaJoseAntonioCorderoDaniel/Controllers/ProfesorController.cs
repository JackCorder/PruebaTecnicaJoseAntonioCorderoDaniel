using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Controllers
{
    [ApiController]
    [Route("api/v1/profesores")]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesoresController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorDTO>>> ObtenerProfesores()
        {
            var profesores = await _profesorService.ObtenerProfesoresAsync();
            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorDTO>> ObtenerProfesorPorId(int id)
        {
            try
            {
                var profesor = await _profesorService.ObtenerProfesorPorIdAsync(id);
                return Ok(profesor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearProfesor([FromBody] ProfesorCreateDTO dto)
        {
            try
            {
                await _profesorService.CrearProfesorAsync(dto.Nombre, dto.Apellido, dto.NumeroIdentificacion, dto.EscuelaID);
                return Ok("Profesor creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarProfesor(int id, [FromBody] ProfesorCreateDTO dto)
        {
            try
            {
                await _profesorService.ActualizarProfesorAsync(id, dto.Nombre, dto.Apellido, dto.NumeroIdentificacion, dto.EscuelaID);
                return Ok("Profesor actualizado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProfesor(int id)
        {
            try
            {
                await _profesorService.EliminarProfesorAsync(id);
                return Ok("Profesor eliminado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
