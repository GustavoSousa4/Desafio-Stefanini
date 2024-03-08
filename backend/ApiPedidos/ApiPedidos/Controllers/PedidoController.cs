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
            return Ok(await _service.CreateOrder(pedido));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] PedidoRequestDto pedidoRequestDto)
        {        
                return Ok(await _service.UpdateOrder(pedidoRequestDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
                return Ok(await _service.DeleteOrder(id));
        }

    }
}
