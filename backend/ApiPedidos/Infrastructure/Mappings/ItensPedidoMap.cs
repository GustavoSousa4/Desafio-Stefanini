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
    public class ItensPedidoMap : IEntityTypeConfiguration<ItensPedido>
    {
        public void Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            builder.ToTable("ItensPedido");
            
            builder.HasKey("Id");

            builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("Id");

            builder.Property(x => x.Quantidade).HasColumnName("Quantidade").HasColumnType("INT").IsRequired();
            
            builder.HasOne(x => x.Produto).WithMany().HasForeignKey(x => x.IdProduto).HasConstraintName("FK_Produto_ItensPedido").OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Pedido).WithMany(x => x.ItensPedido)
                .HasForeignKey(x => x.IdPedido)
                .HasConstraintName("FK_Pedido_ItensPedido")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
