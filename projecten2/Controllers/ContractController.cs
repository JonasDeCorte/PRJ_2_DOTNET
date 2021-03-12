using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projecten2.filter;
using projecten2.Models.Domain;
using projecten2.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace projecten2.Controllers
{
    public class ContractController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IContractTypeRepository _contractTypeRepository;

        public ContractController(IGebruikerRepository gebruikerRepository, IContractTypeRepository contractTypeRepository)
        {
            _gebruikerRepository = gebruikerRepository;
            _contractTypeRepository = contractTypeRepository;
        }

        // GET: ContractController
        [Authorize]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Index(Klant klant)
        {
            IEnumerable<Contract> contracten = klant.Contracten;
            if (contracten == null)
            {
                return NotFound();
            }
            return View(contracten);
        }

        // GET: ContractController/Details 
        public IActionResult Details(int id)
        {
            Contract contract = _gebruikerRepository.GetByContractNr(id);
            if (contract == null) { return NotFound(); }
            return View(contract);
        }

        // GET: ContractController/Create
        [Authorize]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Create(Klant klant)
        {
            ViewData["contractTypes"] = GetContractTypesAsSelectList();
            return View(new ContractEditViewModel());
        }

        // POST: ContractController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Create(ContractEditViewModel cevm, Klant klant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Contract contract = new Contract();
                    MapContractEditViewModelToContract(cevm, contract);
                    klant.VoegContractToe(contract);
                    _gebruikerRepository.AddContract(contract);
                    _gebruikerRepository.SaveChanges();
                    TempData["message"] = $"Het contract ${contract.ContractTitel} is aangemaakt.";
                }
                catch
                {
                    TempData["error"] = "Sorry, er is iets fout gelopen en het contract is niet aangemaakt...";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["contractTypes"] = GetContractTypesAsSelectList();
            return View(nameof(Create), cevm);
        }



        // GET: ContractController/Delete
        public IActionResult Delete()
        {
            return View();
        }

        // POST: ContractController/Delete



        private void MapContractEditViewModelToContract(ContractEditViewModel contractEditViewModel, Contract contract )
        {
            
            contract.ContractTitel = contractEditViewModel.ContractTitel;
            contract.StartDatum = contractEditViewModel.StartDatum;
            contract.Doorlooptijd = contractEditViewModel.DoorloopTijd;
            contract.ContractTypeId = contractEditViewModel.ContractTypeId;
            contract.EindDatum = contractEditViewModel.StartDatum.AddYears(contractEditViewModel.DoorloopTijd);
        }

        private SelectList GetContractTypesAsSelectList()
        {
            return new SelectList(_contractTypeRepository.GetAll(),
            nameof(ContractType.ContractTypeId),
            nameof(ContractType.Naam));
        }
    }
}
