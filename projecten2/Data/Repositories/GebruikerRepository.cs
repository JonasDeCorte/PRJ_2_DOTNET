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
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Gebruiker> _Gebruikers;

        public GebruikerRepository(ApplicationDbContext context)
        {
            _context = context;
            _Gebruikers = _context.Gebruikers;
        }
      

        public IEnumerable<Gebruiker> GetAll()
        {
            return _Gebruikers.OrderBy(x => x.Naam).ToList();
        }

        public Gebruiker GetByEmail(string email)
        {
            return _Gebruikers.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
