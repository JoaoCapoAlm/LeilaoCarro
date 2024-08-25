using System.Net;
using LeilaoCarro.Data.DTO;
using LeilaoCarro.Data.ViewModels;
using LeilaoCarro.Exceptions;
using LeilaoCarro.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeilaoCarro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesErrorResponseType(typeof(ExceptionVM))]
    public class CarroController(CarroService carroService) : ControllerBase
    {
        private readonly CarroService _carroService = carroService;

        /// <summary>
        /// Buscar informações de um carro pelo ID
        /// </summary>
        /// <param name="id">ID do carro</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CarroVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Buscar([FromRoute] int id)
        {
            var carro = await _carroService.BuscarAsync(id);
            if (carro is null)
                throw new AppException("Carro não encontrado", HttpStatusCode.NotFound);

            return Ok(carro);
        }

        /// <summary>
        /// Listagem de carros cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarroVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarAsync()
        {
            var carros = await _carroService.ListarAsync();
            return Ok(carros);
        }
    }
}
