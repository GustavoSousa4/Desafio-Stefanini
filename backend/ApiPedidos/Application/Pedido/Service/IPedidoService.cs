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
        Task<bool> CreateOrder(PedidoRequestDto pedidoRequestDto);
        Task<bool> DeleteOrder(int id);
        Task<bool> UpdateOrder(int id, PedidoRequestDto pedidoRequestDto);
    }
}
