﻿using LeilaoCarro.Data.DTO;
using LeilaoCarro.Data.ViewModels;
using LeilaoCarro.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeilaoCarro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesErrorResponseType(typeof(ExceptionVM))]
    public class UsuarioController(UsuarioService usuarioService) : ControllerBase
    {
        private readonly UsuarioService _usuarioService = usuarioService;

        /// <summary>
        /// Buscar usuario pelo seu ID
        /// </summary>
        /// <param name="id">ID do usuario</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarPorIdAsync([FromRoute] int id)
        {
            var user = await _usuarioService.BuscarAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }


        [HttpPost]
        [ProducesResponseType(typeof(UsuarioVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] NovoUsuarioDTO dto)
        {
            var id = await _usuarioService.AddUsuario(dto);
            var user = await _usuarioService.BuscarAsync(id);
            return Ok(user);
        }
    }
}
