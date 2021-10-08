using GestioneSpese.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.EF.Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasMany(c => c.Spese)
                   .WithOne(s => s.Categoria);

            builder.Property(s => s.Descrizione)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
