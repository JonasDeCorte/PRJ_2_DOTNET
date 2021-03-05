using Microsoft.AspNetCore.Mvc;
using projecten2.Models.Domain;
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

        // GET: ContractController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContractController/Create


        // GET: ContractController/Edit


        // POST: ContractController/Edit
    }
}
