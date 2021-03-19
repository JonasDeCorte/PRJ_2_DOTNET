using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notyf;
        public ContractController(IGebruikerRepository gebruikerRepository, IContractTypeRepository contractTypeRepository, INotyfService notyf)
        {
            _gebruikerRepository = gebruikerRepository;
            _contractTypeRepository = contractTypeRepository;
            _notyf = notyf;
        }

        // GET: ContractController
        [Authorize]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Index(Klant klant)
        {
            _notyf.Information("Kies hier een contract en onderneem een actie.");
            List<Contract> contracten = klant.GetContracten();
            if (contracten == null)
            {
                return NotFound();
            }
            return View(contracten);
        }

        // GET: ContractController/Details 
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Details(int id, Klant klant)
        {
            Contract contract = klant.GetContractById(id); 
            if (contract == null) { return NotFound(); }
            return View(contract);
        }

        // GET: ContractController/Create
        [Authorize]    
        public IActionResult Create()
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
                    ContractType type = _contractTypeRepository.GetContractType(cevm.ContractTypeId);
                    MapContractEditViewModelToContract(cevm, contract);
                    contract.ContractType = type;
                    klant.VoegContractToe(contract);
                    _gebruikerRepository.SaveChanges();
                    _notyf.Success($"Succesvol {contract.ContractTitel} aangemaakt!");
                    TempData["message"] = $"Het contract {contract.ContractTitel} is succesvol aangemaakt.";
                }
                catch
                {
                    _notyf.Error("Oops.. contract is niet aangemaakt. Probeer opnieuw.");
                    TempData["error"] = "Sorry, er is iets fout gelopen. Het contract is niet aangemaakt...";
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
                _notyf.Success($"Succesvol {contract.ContractTitel} stop gezet!");
                TempData["message"] = $"Het contract {contract.ContractTitel} is succesvol stop gezet.";
            }
            catch
            {
                _notyf.Error("Oops.. contract is niet stop gezet. Probeer opnieuw.");
                TempData["error"] = $"Sorry, er is iets fout gelopen. Contract {contract?.ContractTitel} werd niet stop gezet.";
            }
            return RedirectToAction(nameof(Index));
        }

        private void MapContractEditViewModelToContract(ContractEditViewModel contractEditViewModel, Contract contract)
        {
            contract.ContractTitel = contractEditViewModel.ContractTitel;
            contract.StartDatum = contractEditViewModel.StartDatum;
            contract.Doorlooptijd = contractEditViewModel.DoorloopTijd;  
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
