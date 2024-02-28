using Application.Produto.Model.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedido.Model.Request
{
    public class PedidoRequestDto
    {
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public int IdProduto { get; set; }
        public int quantidade { get; set; }

    }
}
