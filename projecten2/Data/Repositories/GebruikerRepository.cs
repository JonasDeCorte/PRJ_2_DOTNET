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
        private readonly DbSet<Klant> _Klanten;

        public GebruikerRepository(ApplicationDbContext context)
        {
            _context = context;
            _Klanten = _context.Klanten;
        }
      

        public IEnumerable<Gebruiker> GetAll()
        {
            return _Klanten.OrderBy(x => x.Naam).ToList();
        }

        public Klant GetByEmail(string email)
        {
            return _Klanten.FirstOrDefault(x => x.Email == email);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
