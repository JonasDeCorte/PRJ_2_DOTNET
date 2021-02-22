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
        public void Add(Gebruiker gebruiker)
        {
            _Gebruikers.Add(gebruiker);
        }

        public IEnumerable<Gebruiker> GetAll()
        {
            return _Gebruikers.OrderBy(x => x.Naam).ToList();
        }

        public Gebruiker GetByGebruikersNaam(string gebruikersnaam)
        {
            return _Gebruikers.FirstOrDefault(x => x.GebruikersNaam.ToLower().Equals(gebruikersnaam.ToLower()));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
