using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Mappers
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>

    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.HasKey(x => x.TicketNr);
            
         
            builder.Property(x => x.Titel).IsRequired().HasMaxLength(50);
          
            builder.Property(x => x.AanmaakDatum).IsRequired();
            builder.Property(x => x.LaatstGewijzigd).IsRequired();
            builder.Property(x => x.Omschrijving).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.Opmerkingen).HasMaxLength(5000);
        
            //builder.HasOne(x => x.Gebruiker).WithMany(t => t.Tickets).OnDelete(DeleteBehavior.SetNull);
           
            builder.HasOne(x => x.Rapport).WithOne(x => x.Ticket).HasForeignKey<Rapport>(x => x.Ticketid).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Contract).WithMany(x => x.Tickets).OnDelete(DeleteBehavior.NoAction);
            

        }
    }
}
