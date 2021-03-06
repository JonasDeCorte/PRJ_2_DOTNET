using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projecten2.Data.Mappers;
using projecten2.Models.Domain;

namespace projecten2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // refactoring nodig
        public DbSet<Bedrijf> Bedrijven { get; set; }
        
        public DbSet<Contract> Contracten { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }  
        public DbSet<Rapport> Rapporten { get; set; }     
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<GebruikerLogin> GebruikerLogins { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }    
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<AppFile> File { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring = @"Server=.;Database=Projecten2;Integrated Security=true;";
            optionsBuilder.UseSqlServer(connectionstring);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker").HasKey(x => x.GebruikersId);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BedrijfConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new ContractTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RapportConfiguration());
            modelBuilder.ApplyConfiguration(new KlantConfiguration());
            modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
            
        }
    }
}
