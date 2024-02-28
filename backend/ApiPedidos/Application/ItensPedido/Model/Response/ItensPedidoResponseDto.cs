using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ItensPedido.Model.Response
{
    public class ItensPedidoResponseDto
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set;}
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
