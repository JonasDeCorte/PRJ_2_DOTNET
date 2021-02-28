using Microsoft.AspNetCore.Mvc;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Controllers
{
    public class GebruikerController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        public GebruikerController(IGebruikerRepository gebruikerRepository) 
        {
            _gebruikerRepository = gebruikerRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<Gebruiker> gebruikers = _gebruikerRepository.GetAll().OrderBy(b => b.Naam);
            return View(gebruikers);
        }
    }
}
