using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projecten2.filter;
using projecten2.Models.Domain;
using projecten2.Models.ViewModels;
using System.Collections.Generic;

namespace projecten2.Controllers
{
    public class ContractController : Controller
    {
        private readonly IContractRepository _contractRepository;

        public ContractController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        // GET: ContractController
        public IActionResult Index()
        {
            IEnumerable<Contract> contracten = _contractRepository.GetAll();
            if (contracten == null)
            {
                return NotFound();
            }
            return View(contracten);
        }

        // GET: ContractController/Details 
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: ContractController/Create
        [Authorize]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Create()
        {

            return View(new ContractEditViewModel());
        }

        // POST: ContractController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContractEditViewModel cemv)
        {
            return View();
        }



        // GET: ContractController/Delete
        public IActionResult Delete()
        {
            return View();
        }

        // POST: ContractController/Delete

    }
}
