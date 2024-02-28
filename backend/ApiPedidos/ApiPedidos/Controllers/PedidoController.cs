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

        //[HttpGet("GetAll")]
        //public async Task<IEnumerable<Pedido>> GetAllAsync()
        //{
        //    //return await _repository.GetAll();
        //}

        [HttpGet("Pedidos/{id}")]
        public async Task<PedidoResponseDto> GetByIdAsync([FromRoute] int id)
        {
            return await _service.GetOrderById(id);
        }

        [HttpPost]
        public async Task PostAsync([FromBody] PedidoRequestDto pedido)
        {

            _service.CreateOrder(pedido);
 
        }
    }
}
