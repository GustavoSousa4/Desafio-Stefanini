using System.ComponentModel.DataAnnotations;

namespace Application.Produto.Model.Request
{
    public class ProdutoRequestDto
    {
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
    }
}
