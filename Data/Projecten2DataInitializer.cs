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
