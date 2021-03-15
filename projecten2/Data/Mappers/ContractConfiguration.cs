using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Mappers
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contract");
            builder.HasKey(x => x.ContractNr);

            builder.Property(x => x.ContractTitel).IsRequired().HasMaxLength(50);

         //   builder.Property(x => x.KlantNr).IsRequired();
            builder.Property(x => x.ContractStatus).IsRequired();
            builder.Property(x => x.StartDatum).IsRequired();
            builder.Property(x => x.EindDatum);
            builder.Property(x => x.Doorlooptijd);
        
          
        }
    }
}
