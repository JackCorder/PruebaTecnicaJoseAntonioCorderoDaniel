using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Controllers
{
    [ApiController]
    [Route("api/v1/Escuelas")]
    public class EscuelasController : ControllerBase
    {
        private readonly IEscuelaService _escuelaService;

        public EscuelasController(IEscuelaService escuelaService)
        {
            _escuelaService = escuelaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscuelaCreateDTO>>> ObtenerEscuelas()
        {
            var escuelas = await _escuelaService.ObtenerEscuelasAsync();
            return Ok(escuelas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EscuelaCreateDTO>> ObtenerEscuelaPorId(int id)
        {
            try
            {
                var escuela = await _escuelaService.ObtenerEscuelaPorIdAsync(id);
                return Ok(escuela);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearEscuela([FromBody] EscuelaCreateDTO dto)
        {
            try
            {
                await _escuelaService.CrearEscuelaAsync(dto.Nombre, dto.Descripcion);
                return Ok("Escuela creada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarEscuela(int id, [FromBody] EscuelaCreateDTO dto)
        {
            try
            {
                await _escuelaService.ActualizarEscuelaAsync(id, dto.Nombre, dto.Descripcion);
                return Ok("Escuela actualizada correctamente.");
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
        public async Task<ActionResult> EliminarEscuela(int id)
        {
            try
            {
                await _escuelaService.EliminarEscuelaAsync(id);
                return Ok("Escuela eliminada correctamente.");
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
