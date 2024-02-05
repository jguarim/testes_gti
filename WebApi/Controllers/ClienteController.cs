using Microsoft.AspNetCore.Mvc;
using Projeto.Application.DTO;
using Projeto.Application.Interfaces;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService clienteService;

        public ClienteController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var clientes = await clienteService.GetClientesAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListarPorId/{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            try
            {
                var cliente = await clienteService.GetByIdAsync(id);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] ClienteDTO modelo)
        {
            try
            {
                var id = await clienteService.CreateAsync(modelo);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Alterar")]
        public async Task<IActionResult> Alterar([FromBody] ClienteDTO modelo)
        {
            try
            {
                await clienteService.UpdateAsync(modelo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                await clienteService.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
