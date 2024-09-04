using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Infra.Contexts.Research.Mappings;

public class RelatoryMap: IEntityTypeConfiguration<Relatory>
{
    public void Configure(EntityTypeBuilder<Relatory> builder)
    {
        builder.ToTable("Relatories");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired(true);
        
        /*===================Seeding===================*/

        builder.HasData(
            new Relatory { Title = "Visão geral da evolução das avaliações" },
            new Relatory { Title = "Avaliações de cada usuário por período" },
            new Relatory { Title = "Distribuição das avaliações por período" },
            new Relatory { Title = "Frequência das avaliações por período de tempo" },
            new Relatory { Title = "Número adequado de clusters de usuário" },
            new Relatory { Title = "Média da experiência do usuário ao longo do tempo" }
            );
    }
}