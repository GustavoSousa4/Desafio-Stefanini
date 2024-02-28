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

        public async Task<bool> CreateOrder(PedidoRequestDto pedidoRequestDto)
        {
            ValidateOrder(pedidoRequestDto);

            var novoPedido = new Domain.Entities.Pedido(pedidoRequestDto.NomeCliente, pedidoRequestDto.EmailCliente, pedidoRequestDto.Pago, DateTime.Now);
            await _repository.Create(novoPedido);

            var pedido = novoPedido.Id;

            await _itensPedidoRepository.Create(new Domain.Entities.ItensPedido(pedidoRequestDto.IdProduto, pedido, pedidoRequestDto.quantidade));

            return true;
        }
        public async Task<bool> DeleteOrder(int id)
        {
            await _repository.Delete(id);

            return true;
        }
        public async Task<bool> UpdateOrder(int id, PedidoRequestDto pedidoRequestDto)
        {
            ValidateOrder(pedidoRequestDto);

            var pedido = await _repository.GetById(id);
            var itemPedido = await _itensPedidoRepository.GetByIdPedido(id);

            pedido.NomeCliente = pedidoRequestDto.NomeCliente;
            pedido.EmailCliente = pedidoRequestDto.EmailCliente;
            pedido.Pago = pedidoRequestDto.Pago;

            foreach (var item in itemPedido)
            {
                item.IdProduto = pedidoRequestDto.IdProduto;
                item.Quantidade = pedidoRequestDto.quantidade;

                await _repository.Update(pedido);
                await _itensPedidoRepository.Update(item);

            }


            return true;
        }

        public async Task<PedidoResponseDto> GetOrderById(int id)
        {
            try
            {
                var pedido = await _repository.GetAll();
                var itens = await _itensPedidoRepository.GetAll();
                var pedidoSelecionado = pedido.SingleOrDefault(x => x.Id == id);
                var itensSelecinados = itens.SingleOrDefault(x => x.IdPedido == pedidoSelecionado.Id);
                var produto = await _produtoService.GetProductById(itensSelecinados.IdProduto);

                var response = new PedidoResponseDto
                {
                    Id = itensSelecinados.Id,
                    NomeCliente = pedidoSelecionado.NomeCliente,
                    EmailCliente = pedidoSelecionado.EmailCliente,
                    Pago = pedidoSelecionado.Pago,
                    ItensPedidos = new List<ItensPedidoResponseDto>()
                    {
                        new ItensPedidoResponseDto
                        {
                            Id = itensSelecinados.Id,
                            IdProduto = produto.Id,
                            NomeProduto = produto.NomeProduto,
                            ValorUnitario = produto.Valor,
                            Quantidade = itensSelecinados.Quantidade
                        }
                    }

                };

                response.ValorTotal = response.ItensPedidos.Sum(x => x.Quantidade * x.ValorUnitario);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar pedidos");
            }
        }
        public async Task<List<PedidoResponseDto>> GetAllOrders()
        {
            try
            {
                var pedidos = await _repository.GetAll();
                var response = new List<PedidoResponseDto>();
                foreach (var pedido in pedidos)
                {
                    var itens = await _itensPedidoRepository.GetByIdPedido(pedido.Id);
                    var itensPedidoResponse = new List<ItensPedidoResponseDto>();

                    foreach (var item in itens)
                    {
                        var produto = await _produtoService.GetProductById(item.IdProduto);
                        var itemResponse = new ItensPedidoResponseDto
                        {
                            Id = item.Id,
                            IdProduto = produto.Id,
                            NomeProduto = produto.NomeProduto,
                            Quantidade = item.Quantidade,
                            ValorUnitario = produto.Valor
                        };
                        itensPedidoResponse.Add(itemResponse);
                    }
                    var pedidoResponse = new PedidoResponseDto
                    {
                        Id = pedido.Id,
                        NomeCliente = pedido.NomeCliente,
                        EmailCliente = pedido.EmailCliente,
                        Pago = pedido.Pago,
                        ItensPedidos = itensPedidoResponse
                    };
                    pedidoResponse.ValorTotal = pedidoResponse.ItensPedidos.Sum(x => x.Quantidade * x.ValorUnitario);

                    response.Add(pedidoResponse);
                }
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private async void ValidateOrder(PedidoRequestDto orderDto)
        {
            var produtoExist = await _produtoService.GetAllProducts();
            if (orderDto.IdProduto <= 0)
                throw new Exception("Digite um id válido");
            if (!produtoExist.Any())
                throw new Exception("O id do produto é inválido");
            if (orderDto.quantidade < 0)
                throw new Exception("Digite uma quantidade válida do produto");
            if (orderDto.EmailCliente is null)
                throw new Exception("Email do cliente inválido");
            if (orderDto.NomeCliente is null)
                throw new Exception("Nome do cliente inválido");
        }

    }
}

