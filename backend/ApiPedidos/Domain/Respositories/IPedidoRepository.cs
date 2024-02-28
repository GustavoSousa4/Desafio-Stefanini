using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Respositories
{
    public interface IPedidoRepository
    {
        Task<bool> Create(Pedido pedido);
        Task<bool> Update(Pedido pedido);
        Task<bool> Delete(int id);
        Task<IEnumerable<Pedido>> GetAll();
        Task<Pedido> GetById(int id);
    }
}
