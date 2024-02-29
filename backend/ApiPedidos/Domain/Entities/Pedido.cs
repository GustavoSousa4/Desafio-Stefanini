namespace Domain.Entities
{
    public class Pedido
    {
        public Pedido()
        {
            
        }
        public Pedido(string nomeCliente, string emailCliente,  bool pago, DateTime dataCriacao)
        {
            NomeCliente = nomeCliente;
            EmailCliente = emailCliente;
            Pago = pago;
            DataCriacao = dataCriacao;
        }
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual ICollection<ItensPedido> ItensPedido { get; set; }


    }
}
