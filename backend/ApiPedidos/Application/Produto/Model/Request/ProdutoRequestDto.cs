using System.ComponentModel.DataAnnotations;

namespace Application.Produto.Model.Request
{
    public class ProdutoRequestDto
    {
        public decimal Valor { get; set; }
        public string NomeProduto { get; set; }
    }
}
