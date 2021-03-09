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
             if (!_dbContext.ContractTypes.Any())
            {
                ContractType type = new ContractType("test", "open", DateTime.Now, DateTime.Now.AddDays(30), DateTime.Now.AddDays(15), 500.00);
                _dbContext.ContractTypes.AddRange(type);
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
            if (!_dbContext.Gebruikers.Any())
            {       
                Klant jan = new Klant() { GebruikersNaam = "jan@hogent.be", Naam = "Peeters", Voornaam = "Jan", Email = "jan@gmail.com" };
                Klant peter = new Klant() { GebruikersNaam = "peter@hogent.be",  Naam = "Claeyssens", Voornaam = "Peter", Email = "peter@hogent.be" };
                _dbContext.Gebruikers.AddRange(peter, jan);
                _dbContext.SaveChanges();
                for(int i = 0; i < 5; i++)
                {
                    Contract con = new Contract() { ContractStatus = ContractStatus.LOPEND,
                    ContractTitel = $"contract {i}",
                    StartDatum = DateTime.Now,
                    EindDatum = DateTime.Today.AddDays(30),
                    ContractTypeId = _dbContext.ContractTypes.First().ContractTypeId
                }; 
                    jan.VoegContractToe(con);
                    _dbContext.Contracten.Add(con);
                    _dbContext.SaveChanges();
                }

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                    Ticket ticket = new Ticket
                    {
                        ContractId = jan.Contracten.ElementAt(i).ContractNr,
                        Titel = $"Ticket{j}",
                        Omschrijving = $"nog steeds een test ticket{j}",
                        Opmerkingen = $"test ticket{j} ",
                        TicketTypeId = 1
                    };
                    jan.Contracten.ElementAt(i).Tickets.Add(ticket);
                    _dbContext.Tickets.Add(ticket);
                    }
                   
                    _dbContext.SaveChanges();
                }
                for (int i = 5; i < 10; i++)
                {
                    Contract con = new Contract()
                    {
                        ContractStatus = ContractStatus.LOPEND,
                        ContractTitel = $"contract {i}",
                        StartDatum = DateTime.Now,
                        EindDatum = DateTime.Today.AddDays(30),
                        ContractTypeId = _dbContext.ContractTypes.First().ContractTypeId
                    };
                    peter.VoegContractToe(con);
                    _dbContext.Contracten.Add(con);
                    _dbContext.SaveChanges();
                }

                for (int i = 5; i < 10; i++)
                {
                    Ticket ticket = new Ticket
                    {
                        ContractId = peter.Contracten.First().ContractNr,
                        Titel = $"Ticket{i}",
                        Omschrijving = $"nog steeds een test ticket{i}",
                        Opmerkingen = $"test ticket{i} ",
                        TicketTypeId = 1
                    };
                    peter.Contracten.First().Tickets.Add(ticket);
                    _dbContext.Tickets.Add(ticket);
                    _dbContext.SaveChanges();
                }

                SupportManager supportManager = new SupportManager { GebruikersNaam = "supportManager@hogent.be", Email= "supportManager@hogent.be", StartDatumTeWerkStelling = DateTime.Now, Naam = "admin", Voornaam = "admin" };
                _dbContext.Gebruikers.Add(supportManager);
                _dbContext.SaveChanges();
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
            /*
            if (!_dbContext.Contracten.Any())
            {
                ContractType type = new ContractType("test", "open", DateTime.Now, DateTime.Now.AddDays(30), DateTime.Now.AddDays(15), 500.00);
                Contract contract = new Contract("testcontract", 0001, ContractStatus.LOPEND, DateTime.Now, type);
                Contract contract2 = new Contract("testcontract2", 0001, ContractStatus.LOPEND, DateTime.Now, type);
                Contract contract3 = new Contract("testcontract3", 0000, ContractStatus.LOPEND, DateTime.Now, type);
               
                _dbContext.Contracten.AddRange(contract, contract2, contract3);
                _dbContext.SaveChanges();
            }
            */
          
            /*
            if (!_dbContext.Tickets.Any())
            {

                Klant k1 = (Klant)_dbContext.Gebruikers.First();
                Contract contract = _dbContext.Contracten.First();
                Ticket ticket1 = new Ticket("Ticket1", 1, 1, DateTime.Now, "TEST T", "NOG STEEDS EEN TEST TICKET", k1);
                Ticket ticket2 = new Ticket("Ticket2", 2, 1, DateTime.Now, "TEST T2", "NOG STEEDS EEN TEST TICKET2", k1);
                Ticket ticket3 = new Ticket("Ticket3", 3, 1, DateTime.Now, "TEST T3", "NOG STEEDS EEN TEST TICKET3", k1);

                _dbContext.Tickets.AddRange(ticket1, ticket2, ticket3);
                _dbContext.SaveChanges();
            
            }*/
        }
        private async Task InitializeUsersAndCustomers()
        {
            string eMailAddress = "supportManager@hogent.be";
            ApplicationUser admin = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(admin, "P@ssword1");
            await _userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "admin"));
           

            eMailAddress = "jan@gmail.com";
            ApplicationUser klant = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(klant, "P@ssword1");
            await _userManager.AddClaimAsync(klant, new Claim(ClaimTypes.Role, "klant"));

            eMailAddress = "peter@hogent.be";
             klant = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(klant, "P@ssword1");
            await _userManager.AddClaimAsync(klant, new Claim(ClaimTypes.Role, "klant"));

        }
    }
}
