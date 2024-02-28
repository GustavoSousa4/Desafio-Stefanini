using Application.Produto.Model.Request;
using Application.Produto.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Produto.Service
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoResponseDto>> GetAllProducts();
        Task<ProdutoResponseDto> GetProductById(int id);
        Task<ProdutoResponseDto> GetProductByName(string name);
        Task<decimal> GetProductPrice(int id);
        Task<bool> CreateProduct(ProdutoRequestDto produtoRequestDto);
        Task<bool> UpdateProduct(int id, ProdutoRequestDto produtoRequestDto);
        Task<bool> DeleteProduct(int id);
    }
}
