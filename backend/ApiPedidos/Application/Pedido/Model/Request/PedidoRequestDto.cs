﻿using Application.ItensPedido.Model.Resquest;
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
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public IEnumerable<ItensPedidoRequestDto> item { get; set; }

    }
}
