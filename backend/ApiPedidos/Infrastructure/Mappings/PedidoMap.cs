using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");

            builder.HasKey("Id");

            builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("Id");

            builder.Property(x => x.NomeCliente).HasColumnName("NomeCliente").HasColumnType("VARCHAR").HasMaxLength(60).IsRequired();
            
            builder.Property(x => x.EmailCliente).HasColumnName("EmailCliente").HasColumnType("VARCHAR").HasMaxLength(60).IsRequired();

            builder.Property(x => x.Pago).HasColumnName("Pago").HasColumnType("BIT").HasMaxLength(1).IsRequired();

            builder.Property(x => x.DataCriacao).HasColumnType("DATETIME").IsRequired();
        }
    }
}
