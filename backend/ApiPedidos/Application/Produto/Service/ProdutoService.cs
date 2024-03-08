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
            return new ProdutoResponseDto
            {
                Id = response.Id,
                NomeProduto = response.NomeProduto,
                Valor = response.Valor
            };
        }
        public async Task<ProdutoResponseDto> GetProductByName(string name)
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

        public async Task<string> CreateProduct(ProdutoRequestDto produtoRequestDto)
        {
            ValidateRequest(produtoRequestDto);
            try
            {
                await _produtoRepository.Create(new Domain.Entities.Produto(produtoRequestDto.NomeProduto, produtoRequestDto.Valor));
                return "Produto criado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar inserir o produto.", ex);
            }
        }

        public async Task<string> UpdateProduct(int id,ProdutoRequestDto produtoRequestDto)
        {
            ValidateRequest(produtoRequestDto);
            try
            {
                var produto = await _produtoRepository.GetById(id);

                produto.NomeProduto = produtoRequestDto.NomeProduto;
                produto.Valor = produtoRequestDto.Valor;

                await _produtoRepository.Update(produto);
                return $"{produto.NomeProduto} alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar alterar o produto.", ex);
            }
        }

        public async Task<string> DeleteProduct(int id)
        {
            try
            {
                await _produtoRepository.Delete(id);
                return $"Produto excluído com sucesso.";
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao tentar excluir o produto.");
            }
        }

        private void ValidateRequest(ProdutoRequestDto request)
        {
            if (request.NomeProduto == null)
                throw new Exception("Por favor, preencha o campo do nome do produto.");
            if (request.Valor <= 0)
                throw new Exception("O valor do produto deve ser maior que zero.");

        }

    }
}
