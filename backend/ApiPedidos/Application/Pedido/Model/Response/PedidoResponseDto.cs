using Application.ItensPedido.Model.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedido.Model.Response
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public IEnumerable<ItensPedidoResponseDto> ItensPedidos { get; set; }
    }
}
