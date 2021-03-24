using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using projecten2.Controllers;
using projecten2.Models.Domain;
using projecten2.Models.ViewModels;
using projecten2.Tests.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace projecten2.Tests.Controllers
{
    public class TicketControllerTest
    {
        private readonly TicketController _controller;
        private readonly Mock<IGebruikerRepository> _gebruikerRepository;
        private readonly Mock<ITicketTypeRepository> _ticketTypeRepository;
        private readonly Mock<INotyfService> _notyf;
        private readonly DummyDbContext _dummyContext;
        private readonly TicketEditViewModel model;

        public TicketControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _ticketTypeRepository = new Mock<ITicketTypeRepository>();
            _gebruikerRepository = new Mock<IGebruikerRepository>();
            _notyf = new Mock<INotyfService>();

            _controller = new TicketController(_ticketTypeRepository.Object, _gebruikerRepository.Object, _notyf.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
            /*
                        model = new TicketEditViewModel(_dummyContext.Piet.Tickets.First())
                        {
                            Titel = "Ticket1",
                            ContractId = 1,
                            TicketTypeId = 1,
                            Omschrijving = "Omschrijving ticket 1",
                            Opmerkingen = "Opmerkingen ticket 1"
                        };
            */
        }

        #region Index
        [Fact]
        public void Index_PassesOrderedListOfTicketsInViewResultModel()
        {
            _gebruikerRepository.Setup(m => m.GetAllTickets()).Returns(_dummyContext.Tickets);
            var result = Assert.IsType<ViewResult>(_controller.Index(_dummyContext.Piet, false));
            var ticketsInModel = Assert.IsType<List<Ticket>>(result.Model);
            Assert.Equal(3, ticketsInModel.Count);
            Assert.Equal("Ticket3", ticketsInModel[0].Titel);
            Assert.Equal("Ticket2", ticketsInModel[1].Titel);
            Assert.Equal("Ticket1", ticketsInModel[2].Titel);
        }
        #endregion

        #region GET Create
        [Fact]
        public void Create_PassesNewTicketInEditViewModelAndReturnsSelectListsOfTicketTypesAndContracts()
        {
            _ticketTypeRepository.Setup(m => m.GetAll()).Returns(_dummyContext.TicketTypes);
            _gebruikerRepository.Setup(m => m.GetAllContracten()).Returns(_dummyContext.Contracten);
            var result = Assert.IsType<ViewResult>(_controller.Create(_dummyContext.Piet));
            var ticketTypesInViewData = Assert.IsType<SelectList>(result.ViewData["ticketTypes"]);
            var contractenInViewData = Assert.IsType<SelectList>(result.ViewData["contractenKlant"]);
            var ticketEvm = Assert.IsType<TicketEditViewModel>(result.Model);
            Assert.Null(ticketEvm.Titel);
            Assert.Equal(3, ticketTypesInViewData.Count());
            Assert.Equal(2, contractenInViewData.Count());
        }
        #endregion
        [Fact]
        public void Create_InvalidTicket_DoesNotCreateNorPersistsTicketAndRedirectToActionIndex()
        {
            _gebruikerRepository.Setup(m => m.AddTicket(It.IsAny<Ticket>()));
            var ticketEvm = new TicketEditViewModel(new Ticket("InvalidTicket"))
            {
                Omschrijving = null
            };
            var result = Assert.IsType<RedirectToActionResult>(_controller.Create(ticketEvm, _dummyContext.Piet));
            Assert.Equal("Index", result.ActionName);
            _gebruikerRepository.Verify(m => m.AddTicket(It.IsAny<Ticket>()), Times.Never());
            _gebruikerRepository.Verify(m => m.SaveChanges(), Times.Never());
        }
       
       
    }
}

/*
#region POST Create
[Fact]
public void Create_ValidTicket_CreatesAndPersistsTicketAndRedirectsToActionIndex()
{
_ticketRepository.Setup(m => m.Add(It.IsAny<Ticket>()));
var ticketEvm = new TicketEditViewModel(new Ticket("ValidTicket")
{
 Contract = _dummyContext.Contracten.Last(),
 TicketType = _dummyContext.TicketTypes.Last(),
 Omschrijving = "Omschrijving TicketTest",
 Opmerkingen = "Opmerkingen TicketTest",
});
var result = Assert.IsType<RedirectToActionResult>(_controller.Create(ticketEvm));
Assert.Equal("Index", result?.ActionName);
_ticketRepository.Verify(m => m.Add(It.IsAny<Ticket>()), Times.Once());
_ticketRepository.Verify(m => m.SaveChanges(), Times.Once());
}


#endregion

#region GET Edit
[Fact]
        public void Edit_PassesTicketInEditViewModelAndReturnsSelectListsOfTicketTypesAndContracts()
        {
            
            _gebruikerRepository.Setup(m => m.GetByTicketNr(1)).Returns(_dummyContext.t);
            _ticketTypeRepository.Setup(m => m.GetAll()).Returns(_dummyContext.TicketTypes);
            _gebruikerRepository.Setup(m => m.GetAllContracten()).Returns(_dummyContext.Contracten);
            var result = Assert.IsType<ViewResult>(_controller.Edit(1));
            var ticketEvm = Assert.IsType<TicketEditViewModel>(result.Model);
            var ticketTypesInViewData = Assert.IsType<SelectList>(result.ViewData["ticketTypes"]);     
            var contractenInViewData = Assert.IsType<SelectList>(result.ViewData["contractenKlant"]);
            Assert.Equal("Titel ticketP1", ticketEvm.Titel);
            Assert.Equal(1, ticketEvm.TicketTypeId);
            Assert.Equal(3, ticketTypesInViewData.Count());
            Assert.Equal(1, ticketEvm.ContractId);
            Assert.Equal(3, contractenInViewData.Count());
        }
#endregion
 [Fact]
        public void Edit_InvalidEdit_DoesNotChangeNorPersistsBrewerAndRedirectsToActionIndex()
        {
            _gebruikerRepository.Setup(m => m.GetByTicketNr(1)).Returns(_dummyContext.t);
            var ticketEvm = new TicketEditViewModel(_dummyContext.t)
            {
                Omschrijving = null
            };
            var result = Assert.IsType<RedirectToActionResult>(_controller.Edit(1));
            var TicketPiet1 = _dummyContext.t;
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Ticket1", TicketPiet1.Titel);
            Assert.Equal("Omschrijving ticket 1", TicketPiet1.Omschrijving);
            _gebruikerRepository.Verify(m => m.SaveChanges(), Times.Never());
        }



 [Fact]
        public void Edit_ValidEdit_UpdatesAndPersistsTicketAndRedirectsToActionIndex()
        {
            _gebruikerRepository.Setup(m => m.GetByTicketNr(1)).Returns(_dummyContext.t);
            var ticketEvm = new TicketEditViewModel(_dummyContext.t)
            {
                Omschrijving = "nieuwe omschrijving ticket"
            };
            var result = Assert.IsType<RedirectToActionResult>(_controller.Edit(1));
            var TicketPiet1 = _dummyContext.t;
            Assert.Equal("Index", result?.ActionName);
            Assert.Equal("Ticket1", _dummyContext.t.Titel);
            Assert.Equal("nieuwe omschrijving ticket", _dummyContext.t.Omschrijving);
            _gebruikerRepository.Verify(m => m.SaveChanges(), Times.Once());
        }
#endregion
}
}
*/