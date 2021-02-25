using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public interface IGebruikerRepository
    {
<<<<<<< Updated upstream
        Gebruiker GetByEmail(string email);
=======
        Gebruiker GetByEmail(string gebruikersnaam);
>>>>>>> Stashed changes
        IEnumerable<Gebruiker> GetAll();
        
        void SaveChanges();
    }
}
