using Application.Produto.Model.Request;
using Application.Produto.Model.Response;
using Domain.Respositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Produto.Service

{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<ProdutoResponseDto>> GetAllProducts()
        {
            var result = await _produtoRepository.GetAll();
            return result.Select(x => new ProdutoResponseDto
            {
                Id = x.Id,
                NomeProduto = x.NomeProduto,
                Valor = x.Valor
            });
        }
        public async Task<ProdutoResponseDto> GetProductById(int id)
        {
            var response = await _produtoRepository.GetById(id);
            return new ProdutoResponseDto { 
                Id = response.Id, 
                NomeProduto = response.NomeProduto,
                Valor = response.Valor 
            };
        }
        public async Task<ProdutoResponseDto>GetProductByName(string name)
        {
            var response = await _produtoRepository.GetByName(name);
            return new ProdutoResponseDto
            {
                Id = response.Id,
                NomeProduto = response.NomeProduto,
                Valor = response.Valor
            };
        }
        public async Task<decimal> GetProductPrice(int id)
        {
            return await _produtoRepository.GetPriceProduct(id);
        }

        public async Task<bool> CreateProduct(ProdutoRequestDto produtoRequestDto)
        {
            ValidateRequest(produtoRequestDto);
            await _produtoRepository.Create(new Domain.Entities.Produto(produtoRequestDto.NomeProduto, produtoRequestDto.Valor));
            return true;
        }

        public async Task<bool> UpdateProduct(int id, ProdutoRequestDto produtoRequestDto)
        {
            ValidateRequest(produtoRequestDto);

            var produto = await _produtoRepository.GetById(id);

            produto.NomeProduto = produtoRequestDto.NomeProduto;
            produto.Valor = produtoRequestDto.Valor;

            await _produtoRepository.Update(produto);
            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            await _produtoRepository.Delete(id);
            return true;
        }

        private void ValidateRequest(ProdutoRequestDto request)
        {
            if (request.NomeProduto == null)
                throw new Exception("Por favor, preencha o campo do nome do produto.");
            if(request.Valor <= 0)
                throw new Exception("O valor do produto deve ser maior que zero.");

        }

    }
}
