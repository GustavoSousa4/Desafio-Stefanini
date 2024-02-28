using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Respositories
{
    public interface IItensPedidoRepository
    {
        Task<bool> Create(ItensPedido itensPedido);
        Task<bool> Update(ItensPedido itensPedido);
        Task<bool> Delete(int id);
        Task<IEnumerable<ItensPedido>> GetAll();
        Task<ItensPedido> GetById(int id);
        Task<List<ItensPedido>> GetByIdPedido(int id);
    }
}
