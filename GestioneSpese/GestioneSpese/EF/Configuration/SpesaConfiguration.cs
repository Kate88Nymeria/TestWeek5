using GestioneSpese.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneSpese.EF.Configuration
{
    public class SpesaConfiguration : IEntityTypeConfiguration<Spesa>
    {
        public void Configure(EntityTypeBuilder<Spesa> builder)
        {
            builder.HasOne(s => s.Categoria)
                   .WithMany(c => c.Spese);

            
            builder.Property(s => s.Data)
                   .IsRequired();
            
            builder.Property(s => s.Descrizione)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(s => s.Utente)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.Importo)
                   .IsRequired();

            builder.Property(s => s.Approvato)
                .IsRequired();
        }
    }
}