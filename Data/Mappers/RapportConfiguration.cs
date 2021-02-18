using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Mappers
{
    public class RapportConfiguration : IEntityTypeConfiguration<Rapport>
    {
        public void Configure(EntityTypeBuilder<Rapport> builder)
        {
            builder.ToTable("Rapport");
            builder.HasKey(x => x.RapportNr);
            builder.Property(x => x.RapportNaam).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Beschrijving).IsRequired().HasMaxLength(25);
        }
    }
}
