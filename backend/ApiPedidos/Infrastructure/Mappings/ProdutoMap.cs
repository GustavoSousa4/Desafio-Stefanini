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
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("Id");

            builder.Property(x => x.NomeProduto).IsRequired().HasColumnType("VARCHAR").HasMaxLength(20);

            builder.Property(x => x.Valor).IsRequired().HasColumnType("DECIMAL(10,2)");
        }
    }
}
