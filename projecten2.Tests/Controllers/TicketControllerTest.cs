/* using Microsoft.AspNetCore.Mvc;
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
        private readonly Mock<ITicketRepository> _ticketRepository;
        private readonly Mock<ITicketTypeRepository> _ticketTypeRepository;
        private readonly Mock<IContractRepository> _contractRepository;
        private readonly DummyDbContext _dummyContext;

        public TicketControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _ticketRepository = new Mock<ITicketRepository>();
            _ticketTypeRepository = new Mock<ITicketTypeRepository>();
            _contractRepository = new Mock<IContractRepository>();
            _controller = new TicketController(_ticketRepository.Object, _ticketTypeRepository.Object, _contractRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        #region Index
        [Fact]
        public void Index_PassesOrderedListOfTicketsInViewResultModel()
        {
            _ticketRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Tickets);
            var result = Assert.IsType<ViewResult>(_controller.Index());
            var ticketsInModel = Assert.IsType<List<Ticket>>(result.Model);
            Assert.Equal(4, ticketsInModel.Count);
            Assert.Equal("Titel ticketP1", ticketsInModel[0].Titel);
            Assert.Equal("Titel ticketP2", ticketsInModel[1].Titel);
            Assert.Equal("Titel ticketP3", ticketsInModel[2].Titel);
            Assert.Equal("Titel ticketS1", ticketsInModel[3].Titel);
        }
        #endregion
        /*
        #region GET Create
        [Fact]
        public void Create_PassesNewTicketInEditViewModelAndReturnsSelectListsOfTicketTypesAndContracts()
        {
            _ticketTypeRepository.Setup(m => m.GetAll()).Returns(_dummyContext.TicketTypes);
            _contractRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Contracten);
            var result = Assert.IsType<ViewResult>(_controller.Create());
            var ticketTypesInViewData = Assert.IsType<SelectList>(result.ViewData["ticketTypes"]);
            var contractenInViewData = Assert.IsType<SelectList>(result.ViewData["contractenKlant"]);
            var ticketEvm = Assert.IsType<TicketEditViewModel>(result.Model);
            Assert.Null(ticketEvm.Titel);
            Assert.Equal(3, ticketTypesInViewData.Count());
            Assert.Equal(3, contractenInViewData.Count());
        }
        #endregion
        */
/* #region POST Create
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

 [Fact]
 public void Create_InvalidTicket_DoesNotCreateNorPersistsTicketAndRedirectToActionIndex()
 {
     _ticketRepository.Setup(m => m.Add(It.IsAny<Ticket>()));
     var ticketEvm = new TicketEditViewModel(new Ticket("InvalidTicket"))
     {
         Omschrijving = null
     };
     var result = Assert.IsType<RedirectToActionResult>(_controller.Create(ticketEvm));
     Assert.Equal("Index", result.ActionName);
     _ticketRepository.Verify(m => m.Add(It.IsAny<Ticket>()), Times.Never());
     _ticketRepository.Verify(m => m.SaveChanges(), Times.Never());
 }
 #endregion

 #region GET Edit
 [Fact]
 public void Edit_PassesTicketInEditViewModelAndReturnsSelectListsOfTicketTypesAndContracts()
 {
     _ticketRepository.Setup(m => m.GetByTicketNr(1)).Returns(_dummyContext.TicketPiet1);
     _ticketTypeRepository.Setup(m => m.GetAll()).Returns(_dummyContext.TicketTypes);
     _contractRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Contracten);
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

 #region POST Edit
 [Fact]
 public void Edit_ValidEdit_UpdatesAndPersistsTicketAndRedirectsToActionIndex()
 {
     _ticketRepository.Setup(m => m.GetByTicketNr(1)).Returns(_dummyContext.TicketPiet1);
     var ticketEvm = new TicketEditViewModel(_dummyContext.TicketPiet1)
     {
         Omschrijving = "nieuwe omschrijving ticket"
     };
     var result = Assert.IsType<RedirectToActionResult>(_controller.Edit(1));
     var TicketPiet1 = _dummyContext.TicketPiet1;
     Assert.Equal("Index", result?.ActionName);
     Assert.Equal("TicketPiet1", TicketPiet1.Titel);
     Assert.Equal("nieuwe omschrijving ticket", TicketPiet1.Omschrijving);
     _ticketRepository.Verify(m => m.SaveChanges(), Times.Once());
 }

 [Fact]
 public void Edit_InvalidEdit_DoesNotChangeNorPersistsBrewerAndRedirectsToActionIndex()
 {
     _ticketRepository.Setup(m => m.GetByTicketNr(1)).Returns(_dummyContext.TicketPiet1);
     var ticketEvm = new TicketEditViewModel(_dummyContext.TicketPiet1)
     {
         Omschrijving = null
     };
     var result = Assert.IsType<RedirectToActionResult>(_controller.Edit(1));
     var TicketPiet1 = _dummyContext.TicketPiet1;
     Assert.Equal("Index", result.ActionName);
     Assert.Equal("TicketPiet1", TicketPiet1.Titel);
     Assert.Equal("Omschrijving ticketP1", TicketPiet1.Omschrijving);
     _ticketRepository.Verify(m => m.SaveChanges(), Times.Never());
 }
 #endregion
}
}
*/