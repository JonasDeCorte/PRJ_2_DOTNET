using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Mappers
{
    public class KlantConfiguration : IEntityTypeConfiguration<Klant>
    {
        public void Configure(EntityTypeBuilder<Klant> builder)
        {


            builder.HasMany(x => x.Bedrijf).WithOne().OnDelete(DeleteBehavior.NoAction);
           // builder.HasMany(x => x.Contracten).WithOne().OnDelete(DeleteBehavior.NoAction);
      
    }
    }
}
