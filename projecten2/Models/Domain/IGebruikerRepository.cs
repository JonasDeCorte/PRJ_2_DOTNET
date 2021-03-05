using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public interface IGebruikerRepository
    {

        Klant GetByEmail(string email);
  

        IEnumerable<Gebruiker> GetAll();
        
        void SaveChanges();
    }
}
