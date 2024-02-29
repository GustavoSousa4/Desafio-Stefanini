using Application.ItensPedido.Model.Response;
using Application.Pedido.Model.Request;
using Application.Pedido.Model.Response;
using Application.Produto.Service;
using Domain.Entities;
using Domain.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedido.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IItensPedidoRepository _itensPedidoRepository;
        private readonly IProdutoService _produtoService;
        public PedidoService(IPedidoRepository repository, IItensPedidoRepository itensPedidoRepository, IProdutoService produtoService)
        {
            _repository = repository;
            _itensPedidoRepository = itensPedidoRepository;

            _produtoService = produtoService;
        }

        public async Task<string> CreateOrder(PedidoRequestDto pedidoRequestDto)
        {
            ValidateOrder(pedidoRequestDto);
            try
            {


                var novoPedido = new Domain.Entities.Pedido()
                {
                    NomeCliente = pedidoRequestDto.NomeCliente,
                    EmailCliente = pedidoRequestDto.EmailCliente,
                    Pago = pedidoRequestDto.Pago,
                    DataCriacao = DateTime.Now,
                    ItensPedido = pedidoRequestDto.item.Select(x => new Domain.Entities.ItensPedido()
                    {
                        IdProduto = x.IdProduto,
                        Quantidade = x.quantidade

                    }).ToList()
                };
                await _repository.Create(novoPedido);

                var pedido = novoPedido.Id;
                return ($"Pedido {pedido} criado com sucesso.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar pedido.", ex);
            }
        }
        public async Task<string> DeleteOrder(int id)
        {
            try
            {
                await _repository.Delete(id);
                return $"Pedido {id} excluído com sucesso.";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar o pedido {id}.", ex);
            }

        }
        public async Task<string> UpdateOrder(int id, PedidoRequestDto pedidoRequestDto)
        {
            ValidateOrder(pedidoRequestDto);
            try
            {

                var pedido = await _repository.GetById(id);
                var itemPedido = await _itensPedidoRepository.GetByIdPedido(id);

                pedido.NomeCliente = pedidoRequestDto.NomeCliente;
                pedido.EmailCliente = pedidoRequestDto.EmailCliente;
                pedido.Pago = pedidoRequestDto.Pago;
                pedido.ItensPedido = pedidoRequestDto.item.Select(x => new Domain.Entities.ItensPedido
                {
                    IdPedido = pedido.Id,
                    IdProduto = x.IdProduto,
                    Quantidade = x.quantidade,

                }).ToList();

                await _repository.Update(pedido);
                return $"Pedido {pedido.Id} alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar o pedido {id}.", ex);
            }
        }

        public async Task<PedidoResponseDto> GetOrderById(int id)
        {
            try
            {
                var pedido = await _repository.GetById(id);
                var response = new PedidoResponseDto
                {
                    Id = pedido.Id,
                    NomeCliente = pedido.NomeCliente,
                    EmailCliente = pedido.EmailCliente,
                    Pago = pedido.Pago,
                    ItensPedidos = pedido.ItensPedido.Select(c =>
                        new ItensPedidoResponseDto
                        {
                            Id = c.Id,
                            IdProduto = c.IdProduto,
                            NomeProduto = c.Produto.NomeProduto,
                            ValorUnitario = c.Produto.Valor,
                            Quantidade = c.Quantidade
                        }
                    ).ToList(),
                    ValorTotal = pedido.ItensPedido.Sum(z => z.Quantidade * z.Produto.Valor)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar pedidos.");
            }
        }
        public async Task<List<PedidoResponseDto>> GetAllOrders()
        {
            try
            {
                var pedido = await _repository.GetAll();

                var response = pedido.Select(x => new PedidoResponseDto
                {
                    Id = x.Id,
                    NomeCliente = x.NomeCliente,
                    EmailCliente = x.EmailCliente,
                    Pago = x.Pago,
                    ItensPedidos = x.ItensPedido.Select(c =>
                        new ItensPedidoResponseDto
                        {
                            Id = c.Id,
                            IdProduto = c.IdProduto,
                            NomeProduto = c.Produto.NomeProduto,
                            ValorUnitario = c.Produto.Valor,
                            Quantidade = c.Quantidade
                        }
                    ).ToList(),
                    ValorTotal = x.ItensPedido.Sum(z => z.Quantidade * z.Produto.Valor)
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private async void ValidateOrder(PedidoRequestDto orderDto)
        {
            var produtoExist = await _produtoService.GetAllProducts();
            if (!produtoExist.Any())
                throw new Exception("O id do produto é inválido.");
            if (orderDto.item.Any(x => x.quantidade < 1))
                throw new Exception("Digite uma quantidade válida do produto.");
            if (orderDto.EmailCliente is null)
                throw new Exception("Email do cliente inválido.");
            if (orderDto.NomeCliente is null)
                throw new Exception("Nome do cliente inválido.");
        }

    }
}

