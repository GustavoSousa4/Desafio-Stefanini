using Application.Pedido.Model.Request;
using Application.Pedido.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedido.Service
{
    public interface IPedidoService
    {
        Task<PedidoResponseDto> GetOrderById(int id);
        Task<List<PedidoResponseDto>> GetAllOrders();
        Task<string> CreateOrder(PedidoRequestDto pedidoRequestDto);
        Task<string> DeleteOrder(int id);
        Task<string> UpdateOrder(int id, PedidoRequestDto pedidoRequestDto);
    }
}
