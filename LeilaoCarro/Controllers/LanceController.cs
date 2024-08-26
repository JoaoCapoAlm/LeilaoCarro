using LeilaoCarro.Data.DTO;
using LeilaoCarro.Data.ViewModels;
using LeilaoCarro.Models;
using LeilaoCarro.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeilaoCarro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesErrorResponseType(typeof(ExceptionVM))]
    public class LanceController(LanceService lanceService) : ControllerBase
    {
        private readonly LanceService _lanceService = lanceService;

        [HttpPost]
        public async Task<IActionResult> AdicionarAsync([FromBody] NovoLanceDTO dto)
        {
            var id = await _lanceService.AdicionarAsync(dto);
            var lance = await _lanceService.ObterAsync(id);
            return Ok(lance);
        }

        [HttpGet("carro/{id:int}")]
        [ProducesResponseType(typeof(IEnumerable<LanceCompletoVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarPorCarro(int id)
        {
            var lances = await _lanceService.ObterPorIdCarroAssync(id);
            return Ok(lances);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(LanceCompletoVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Buscar(int id)
        {
            var lance = await _lanceService.ObterAsync(id);
            if(lance is null)
                return NotFound();

            return Ok(lance);
        }
    }
}
