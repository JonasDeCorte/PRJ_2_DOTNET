using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Mappers
{
    public class ContractTypeConfiguration : IEntityTypeConfiguration<ContractType>
    {
        public void Configure(EntityTypeBuilder<ContractType> builder)
        {
            builder.ToTable("ContractType");
            builder.HasKey(x => x.ContractTypeId);
            builder.Property(x => x.Naam).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Status).HasMaxLength(50).IsRequired();
           
        }
    }
}
