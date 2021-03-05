using Microsoft.AspNetCore.Identity;
using projecten2.Models.Domain;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace projecten2.Data
{
    public class Projecten2DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Gebruiker> _userManager;

        public Projecten2DataInitializer(ApplicationDbContext dbContext, UserManager<Gebruiker> userManager)
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
            if (!_dbContext.Bedrijven.Any())
            {
                String[] nummers = { "+32 517 47 89 65", "+32 586 44 88 66", "+32 886 54 89 63" };
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
                Contract contract = new Contract("testcontract", 0001, ContractStatus.LOPEND, DateTime.Now, type);
                Contract contract2 = new Contract("testcontract2", 0001, ContractStatus.LOPEND, DateTime.Now, type);
                Contract contract3 = new Contract("testcontract3", 0000, ContractStatus.LOPEND, DateTime.Now, type);
                _dbContext.Contracten.AddRange(contract, contract2, contract3);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.TicketTypes.Any())
            {
                TicketType type_1 = new TicketType("PRODUCTIE_GEIMPACTEERD_BINNEN_2U_OPLOSSING", "Hoog");
                TicketType type_2 = new TicketType("PRODUCTIE_ZAL_STIL_VALLEN_BINNEN_4U_OPLOSSING", "Medium");
                TicketType type_3 = new TicketType("GEEN_PRODUCTIE_IMPACT_BINNEN_3DAGEN_ANTWOORD", "Laag");
                _dbContext.TicketTypes.AddRange(type_1, type_2, type_3);
                _dbContext.SaveChanges();
            }
            if (!_dbContext.Tickets.Any())
            {

                Klant k1 = _dbContext.Klanten.First();
                Contract contract = _dbContext.Contracten.First();
                Ticket ticket1 = new Ticket("Ticket1", 1, 1, DateTime.Now, "TEST T", "NOG STEEDS EEN TEST TICKET", k1);
                Ticket ticket2 = new Ticket("Ticket2", 2, 1, DateTime.Now, "TEST T2", "NOG STEEDS EEN TEST TICKET2", k1);
                Ticket ticket3 = new Ticket("Ticket3", 3, 1, DateTime.Now, "TEST T3", "NOG STEEDS EEN TEST TICKET3", k1);

                _dbContext.Tickets.AddRange(ticket1, ticket2, ticket3);
                _dbContext.SaveChanges();
            }
        }
        private async Task InitializeUsersAndCustomers()
        {
            Gebruiker support = new SupportManager
            {
                UserName = "supportManager",
                Voornaam = "Andy",
                Naam = "Depoortere",
                Email = "supportManager@hogent.be",
                Status = true
            };
            await _userManager.CreateAsync(support, "P@ssword1");
            await _userManager.AddClaimAsync(support, new Claim(ClaimTypes.Role, "admin"));

            Klant klant = new Klant
            {

                UserName = "klant",
                Voornaam = "Candace",
                Naam = "Devlieger",
                Email = "klant@hogent.be",
                Status = true,
                KlantNummer = 0001,
                GegevensContactPersonen = "gegevensContactPersonen",
                DatumRegistratie = DateTime.Now
            };
            await _userManager.CreateAsync(klant, "P@ssword1");
            await _userManager.AddClaimAsync(klant, new Claim(ClaimTypes.Role, "klant"));
            Klant klant_2 = new Klant
            {
                Naam = "Test",
                Voornaam = " test",
                UserName = "test",
                Email = "test@hogent.be",
                KlantNummer = 0002,
                GegevensContactPersonen = "testtestest",
                DatumRegistratie = DateTime.Now,

            };
            await _userManager.CreateAsync(klant_2, "P@ssword1");
            await _userManager.AddClaimAsync(klant_2, new Claim(ClaimTypes.Role, "klant"));
            //  _dbContext.Klanten.Add(klant);
            _dbContext.SaveChanges();
        }
    }
}
