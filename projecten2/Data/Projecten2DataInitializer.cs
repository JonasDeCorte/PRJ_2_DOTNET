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
                #region jan tickets contracten
                Contract contract = new Contract(ContractStatus.LOPEND, "contract1", DateTime.Today.AddDays(30), _dbContext.ContractTypes.First().ContractTypeId);
                Ticket ticket;
                for (int j = 1; j < 6; j++)
                {
                    ticket = new Ticket(jan, $"ticket{j}", $"nog steeds een test ticket{j}", $"test ticket{j} ", 1);
                    _dbContext.Tickets.Add(ticket);
                    contract.VoegTicketToe(ticket);
                }
                jan.VoegContractToe(contract);
                _dbContext.Contracten.Add(contract);

                Contract contract2 = new Contract(ContractStatus.LOPEND, "contract2", DateTime.Today.AddDays(30), _dbContext.ContractTypes.First().ContractTypeId);
                for (int j = 6; j < 11; j++)
                {
                    ticket = new Ticket(jan, $"ticket{j}", $"nog steeds een test ticket{j}", $"test ticket{j} ", 1);
                    _dbContext.Tickets.Add(ticket);
                    contract2.VoegTicketToe(ticket);
                }
                jan.VoegContractToe(contract2);
                _dbContext.Contracten.Add(contract2);
                Contract contract3 = new Contract(ContractStatus.LOPEND, "contract3", DateTime.Today.AddDays(30), _dbContext.ContractTypes.First().ContractTypeId);
                for (int j = 11; j < 16; j++)
                {
                    ticket = new Ticket(jan, $"ticket{j}", $"nog steeds een test ticket{j}", $"test ticket{j} ", 1);
                    _dbContext.Tickets.Add(ticket);
                    contract3.VoegTicketToe(ticket);
                }
                jan.VoegContractToe(contract3);
                _dbContext.Contracten.Add(contract3);
                _dbContext.SaveChanges(); 
                #endregion


                #region Peter tickets contracten
                Contract contract4 = new Contract(ContractStatus.LOPEND, "contract1", DateTime.Today.AddDays(30), _dbContext.ContractTypes.First().ContractTypeId);

                for (int j = 16; j < 21; j++)
                {
                    ticket = new Ticket(peter, $"ticket{j}", $"nog steeds een test ticket{j}", $"test ticket{j} ", 1);
                    _dbContext.Tickets.Add(ticket);
                    contract4.VoegTicketToe(ticket);
                }
                peter.VoegContractToe(contract4);
                _dbContext.Contracten.Add(contract4);

                Contract contract5 = new Contract(ContractStatus.LOPEND, "contract2", DateTime.Today.AddDays(30), _dbContext.ContractTypes.First().ContractTypeId);
                for (int j = 21; j < 26; j++)
                {
                    ticket = new Ticket(peter, $"ticket{j}", $"nog steeds een test ticket{j}", $"test ticket{j} ", 1);
                    _dbContext.Tickets.Add(ticket);
                    contract5.VoegTicketToe(ticket);
                }
                peter.VoegContractToe(contract5);
                _dbContext.Contracten.Add(contract5);
                Contract contract6 = new Contract(ContractStatus.LOPEND, "contract3", DateTime.Today.AddDays(30), _dbContext.ContractTypes.First().ContractTypeId);
                for (int j = 26; j < 31; j++)
                {
                    ticket = new Ticket(peter, $"ticket{j}", $"nog steeds een test ticket{j}", $"test ticket{j} ", 1);
                    _dbContext.Tickets.Add(ticket);
                    contract6.VoegTicketToe(ticket);
                }
                peter.VoegContractToe(contract6);
                _dbContext.Contracten.Add(contract6);
                _dbContext.SaveChanges();
                #endregion

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
