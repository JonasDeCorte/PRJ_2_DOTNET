using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projecten2.filter;
using projecten2.Models.Domain;
using projecten2.Models.ViewModels;
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
        public IActionResult Delete(int id)
        {
            ViewData[nameof(Contract.ContractTitel)] = _gebruikerRepository.GetByContractNr(id).ContractTitel;
            return View();
        }

        // POST: ContractController/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Contract contract = null;
            try
            {
                contract = _gebruikerRepository.GetByContractNr(id);
                contract.StopzettenContract(contract);
                _gebruikerRepository.SaveChanges();

                TempData["message"] = $"Je verwijderde succesvol contract {contract.ContractTitel}.";
            }
            catch
            {
                TempData["error"] = $"Sorry, iets ging mis, contract  {contract?.ContractTitel} werd niet verwijderd..";
            }
            return RedirectToAction(nameof(Index));
        }

        private void MapContractEditViewModelToContract(ContractEditViewModel contractEditViewModel, Contract contract)
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
