using Backend_RSV.Data.Mapa;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend_RSV.Controllers.Mapa
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapaController : ControllerBase
    {
        private readonly MapaData _mapaData;

        public MapaController(MapaData mapaData)
        {
            _mapaData = mapaData;
        }

        [HttpGet("marcadores")]
        public async Task<IActionResult> GetMarcadores()
        {
            var marcadores = await _mapaData.ObtenerMarcadoresActivosAsync();
            return Ok(marcadores);
        }

        [HttpGet("marcadores/{id}")]
        public async Task<IActionResult> GetMarcador(int id)
        {
            var marcador = await _mapaData.ObtenerMarcadorPorIdAsync(id);
            if (marcador == null)
                return NotFound(new { message = "Marcador no encontrado." });

            return Ok(marcador);
        }

        [HttpPost("marcadores")]
        public async Task<IActionResult> CrearMarcador([FromBody] MarcadorMapa marcador)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevo = await _mapaData.CrearMarcadorAsync(marcador);
            return CreatedAtAction(nameof(GetMarcador), new { id = nuevo.MarcadorID }, nuevo);
        }

        [HttpPut("marcadores/{id}")]
        public async Task<IActionResult> ActualizarMarcador(int id, [FromBody] MarcadorMapa marcador)
        {
            if (id != marcador.MarcadorID)
                return BadRequest(new { message = "El ID no coincide." });

            var actualizado = await _mapaData.ActualizarMarcadorAsync(marcador);
            if (!actualizado)
                return NotFound(new { message = "Marcador no encontrado." });

            return Ok(new { message = "Marcador actualizado correctamente." });
        }

        [HttpDelete("marcadores/{id}")]
        public async Task<IActionResult> EliminarMarcador(int id)
        {
            var eliminado = await _mapaData.EliminarMarcadorAsync(id);
            if (!eliminado)
                return NotFound(new { message = "Marcador no encontrado." });

            return Ok(new { message = "Marcador eliminado correctamente (borrado lógico)." });
        }
    }

}