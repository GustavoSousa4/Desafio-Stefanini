using Application.ItensPedido.Model.Response;
using Application.Pedido.Model.Request;
using Application.Pedido.Model.Response;
using Application.Produto.Service;
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

            await _repository.Create(new Domain.Entities.Pedido(pedidoRequestDto.NomeCliente, pedidoRequestDto.EmailCliente, pedidoRequestDto.Pago, DateTime.Now));

            var pedido = await _repository.GetAll();


            await _itensPedidoRepository.Create(new Domain.Entities.ItensPedido(pedidoRequestDto.IdProduto, pedido.Select(x => x.Id).First(), pedidoRequestDto.quantidade));

            return true;
        }
        public async Task<PedidoResponseDto> GetOrderById(int id)
        {
            var pedido = await _repository.GetById(id);
            var itens = await _itensPedidoRepository.GetAll();
            var itensSelecinados = itens.FirstOrDefault(x => x.IdPedido == pedido.Id);
            var response = new PedidoResponseDto
            {
                Id = pedido.Id,
                NomeCliente = pedido.NomeCliente,
                EmailCliente = pedido.EmailCliente,
                Pago = pedido.Pago,
                ItensPedidos = new List<ItensPedidoResponseDto>()

            };
            if (response.Id == itensSelecinados.IdPedido)
                response.ItensPedidos.Add(
                new ItensPedidoResponseDto
                {
                    Id = itensSelecinados.Id,
                    IdProduto = itensSelecinados.IdProduto,
                    NomeProduto = _produtoService.GetProductById(itensSelecinados.IdProduto).Result.NomeProduto,
                    ValorUnitario = _produtoService.GetProductById(itensSelecinados.IdProduto).Result.Valor,
                    Quantidade = itensSelecinados.Quantidade
                });
            foreach (var xpto in response.ItensPedidos)
            {
                response.ValorTotal = xpto.Quantidade * xpto.ValorUnitario;
            }
            return response;
        }

        //public async Task<IEnumerable<PedidoResponseDto>> GetAllOrdes()
        //{
        //    var pedido = await _repository.GetAll();
        //    var itens = await _itensPedidoRepository.GetAll();

        //}
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

