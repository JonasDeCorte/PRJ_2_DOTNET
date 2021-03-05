using Microsoft.EntityFrameworkCore;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly DbSet<Gebruiker>_Gebruikers;

        public GebruikerRepository(ApplicationDbContext context)
        {
            _Context = context;
            _Gebruikers = _Context.Gebruikers;
        }
      

        public IEnumerable<Gebruiker> GetAll()
        {
            return _Gebruikers.OrderBy(x => x.Naam).ToList();
        }

        public Gebruiker GetByEmail(string email)
        {
            return _Gebruikers.Include(x => x.Contracten).FirstOrDefault(x => x.Email == email);
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }
    }
}
