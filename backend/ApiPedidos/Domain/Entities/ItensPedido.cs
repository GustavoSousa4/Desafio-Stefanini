namespace Domain.Entities
{
    public class ItensPedido
    {
        public ItensPedido()
        {
                
        }
        public ItensPedido(int idProduto, int idPedido, int quantidade)
        {
            IdProduto = idProduto;
            IdPedido = idPedido;
            Quantidade = quantidade;
        }
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdPedido { get; set; }
        public int Quantidade { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }

    }
}
