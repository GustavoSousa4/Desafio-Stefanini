using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Produto.Model.Response
{
    public class ProdutoResponseDto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
    }
}
