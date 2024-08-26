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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(LanceCompletoVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> AdicionarAsync([FromBody] NovoLanceDTO dto)
        {
            var id = await _lanceService.AdicionarAsync(dto);
            var lance = await _lanceService.ObterAsync(id);
            return Ok(lance);
        }

        /// <summary>
        /// Buscar listagem de lances realizados em um carro espefico
        /// </summary>
        /// <param name="id">ID do carro</param>
        [HttpGet("carro/{id:int}")]
        [ProducesResponseType(typeof(IEnumerable<LanceCompletoVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarPorCarro(int id)
        {
            var lances = await _lanceService.ObterPorIdCarroAssync(id);
            return Ok(lances);
        }

        /// <summary>
        /// Buscar um lance especifico
        /// </summary>
        /// <param name="id">ID do lance</param>
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
