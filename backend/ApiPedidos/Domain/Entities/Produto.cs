namespace Domain.Entities
{
    public class Produto
    {
        public Produto(string nomeProduto, decimal valor) { 
            NomeProduto = nomeProduto;
            Valor = valor;
        }
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
    }
}
