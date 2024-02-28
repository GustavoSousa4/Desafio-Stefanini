using Application.Pedido.Model.Request;
using Application.Pedido.Model.Response;
using Application.Pedido.Service;
using Domain.Entities;
using Domain.Respositories;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Controllers
{
    [ApiController]
    [Route("Pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<List<PedidoResponseDto>> GetAllAsync()
        {
            return await _service.GetAllOrders();
        }

        [HttpGet("{id}")]
        public async Task<PedidoResponseDto> GetByIdAsync([FromRoute] int id)
        {
            return await _service.GetOrderById(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PedidoRequestDto pedido)
        {

            if (await _service.CreateOrder(pedido))
                return Ok("Pedido criado com sucesso");
            return BadRequest("Erro ao criar pedido");

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] PedidoRequestDto pedidoRequestDto)
        {

            if (await _service.UpdateOrder(id, pedidoRequestDto))
                return Ok("Pedido alterado com sucesso");
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (await _service.DeleteOrder(id))
                return Ok("Pedido excluido com sucesso");
            return BadRequest("Erro ao excluir pedido");
        }

    }
}
