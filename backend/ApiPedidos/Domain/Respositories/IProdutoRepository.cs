using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Respositories
{
    public interface IProdutoRepository
    {
        Task<bool> Create(Produto produto);
        Task<bool> Update(Produto produto);
        Task<bool> Delete(int id);
        Task<Produto> GetById(int id);
        Task<Produto> GetByName(string name);
        Task<IEnumerable<Produto>> GetAll();
        Task<decimal> GetPriceProduct(int id);

    }
}
