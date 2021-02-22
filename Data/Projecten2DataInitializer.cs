using Microsoft.AspNetCore.Identity;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace projecten2.Data
{
    public class Projecten2DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public Projecten2DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await InitializeUsersAndCustomers();
                Console.WriteLine("Database Created");
            }
            if (!_dbContext.Bedrijven.Any()) {
                int[] nummers = { 0478194517, 0478202122, 0478232425 };
                Bedrijf BEEGO = new Bedrijf("BEEGO", nummers, "BELGIUM", "OVERAL");
                Bedrijf Microsoft = new Bedrijf("Microsoft", nummers, "BELGIUM", "1K BrusselsAirport");
                Bedrijf Apple = new Bedrijf("Apple", nummers, "BELGIUM", "Gulden-Vlieslaan26/28");
                Bedrijf[] bedrijven = { BEEGO, Microsoft, Apple };
                _dbContext.Bedrijven.AddRange(bedrijven);
                _dbContext.SaveChanges();
                 
            }

            if (!_dbContext.Contracten.Any())
            {
                ContractType type = new ContractType("test", "open", DateTime.Now, DateTime.Now.AddDays(30), DateTime.Now.AddDays(15), 500.00);
                Contract contract = new Contract(new Klant(1, "testgegeven", DateTime.Now), ContractStatus.LOPEND, DateTime.Now, type);
                Contract contract2 = new Contract(new Klant(2, "testgegeven2", DateTime.Now ), ContractStatus.LOPEND, DateTime.Now, type);
                Contract contract3 = new Contract(new Klant(3, "testgegeven3", DateTime.Now ), ContractStatus.LOPEND, DateTime.Now, type);
                _dbContext.Contracten.AddRange(contract, contract2, contract3);
                _dbContext.SaveChanges();
            }


            if (!_dbContext.Tickets.Any()) {

                Klant k1 = _dbContext.Klanten.First();
                Contract contract =  _dbContext.Contracten.First();
                Ticket ticket1 = new Ticket("Ticket1", "TESTEN", DateTime.Now, "TEST T", "NOG STEEDS EEN TEST TICKET", k1, contract);
                Ticket ticket2 = new Ticket("Ticket2", "TESTEN2", DateTime.Now, "TEST T2", "NOG STEEDS EEN TEST TICKET2", k1, contract);
                Ticket ticket3 = new Ticket("Ticket3", "TESTEN3", DateTime.Now, "TEST T3", "NOG STEEDS EEN TEST TICKET3", k1, contract);

                _dbContext.Tickets.AddRange(ticket1, ticket2, ticket3);
                _dbContext.SaveChanges();
            }


        
        }
                private async Task InitializeUsersAndCustomers()
        {
            string eMailAddress = "supportManager@hogent.be";
            IdentityUser user = new IdentityUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));

            eMailAddress = "klant@hogent.be";
            user = new IdentityUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "klant"));



            var customer = new Klant
            {
                Email = eMailAddress,
                Voornaam = "Jan",
                Naam = "De man",

            };
            _dbContext.Klanten.Add(customer);
            _dbContext.SaveChanges();
        }
     
    }
}
