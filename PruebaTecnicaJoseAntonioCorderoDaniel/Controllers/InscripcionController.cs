using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PruebaTecnicaJoseAntonioCorderoDaniel.Controllers
{

    [ApiController]
    [Route("api/v1/inscripciones")]
    public class InscripcionesController : ControllerBase
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionesController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionDTO>>> ObtenerTodas()
        {
            var inscripciones = await _inscripcionService.ObtenerInscripcionesAsync();
            return Ok(inscripciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionDTO>> ObtenerPorId(int id)
        {
            try
            {
                var inscripcion = await _inscripcionService.ObtenerInscripcionPorIdAsync(id);
                return Ok(inscripcion);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] InscripcionCreateDTO dto)
        {
            try
            {
                await _inscripcionService.CrearInscripcionAsync(dto);
                return Ok(new { mensaje = "Inscripción creada correctamente." });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] InscripcionCreateDTO dto)
        {
            try
            {
                await _inscripcionService.ActualizarInscripcionAsync(id, dto);
                return Ok(new { mensaje = "Inscripción actualizada correctamente." });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _inscripcionService.EliminarInscripcionAsync(id);
                return Ok(new { mensaje = "Inscripción eliminada correctamente." });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}