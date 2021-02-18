using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Mappers
{
    public class BedrijfConfiguration : IEntityTypeConfiguration<Bedrijf>
    {
        

        public void Configure(EntityTypeBuilder<Bedrijf> builder)
        {
            var converter = new ValueConverter<int[], string>(
               v => string.Join(";", v),
               v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(val => int.Parse(val)).ToArray());

            builder.ToTable("Bedrijf");
            builder.HasKey(x => x.BedrijfsID);
            builder.Property(x => x.Bedrijfsnaam).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LandHoofdzetel).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Telefoonnummers).HasConversion(converter);
            builder.Property(x => x.Straat).HasMaxLength(25);

        }
    }
}
