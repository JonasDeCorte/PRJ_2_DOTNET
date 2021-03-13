using projecten2.Controllers;
using projecten2.Models.Domain;
using projecten2.Models.ViewModels;
using projecten2.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace projecten2.Tests.Controllers
{
    public class ContractControllerTest

        
    {
        private readonly ContractController _controller;
        private readonly Mock<IContractTypeRepository> _ContractTypeRepository;
        private readonly Mock<IGebruikerRepository> _GebruikerRepository;
        private readonly Mock<ITicketTypeRepository> _TicketTypeRepository;

        private readonly DummyDbContext _dummyContext;

        public ContractControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _ContractTypeRepository = new Mock<IContractTypeRepository>();
            _GebruikerRepository = new Mock<IGebruikerRepository>();
            _TicketTypeRepository = new Mock<ITicketTypeRepository>();
            _controller = new ContractController(_GebruikerRepository.Object, _ContractTypeRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }
        
                    /*    #region -- Index --
                        [Fact]
                        public void {
                        }
                        #endregion

                        #region -- Edit GET --

                        [Fact]
                        public void {
                        }

                        #endregion

                        #region -- Edit POST --
                        [Fact]
                        public void
                        {
                        }   

                        [Fact]
                        public void
                        {
                        }

                        #endregion

                        #region -- Create GET --
                         public void {
                        }

                        #endregion

                        #region -- Create POST --
                        [Fact]
                        public void
                        {
                        }


                        [Fact]
                        public void
                        {
                        }

                        #endregion

                        */

    #region -- Delete GET --
    [Fact]
        public void Delete_PassesNameOfContractInViewData()
        {
            _GebruikerRepository.Setup(m => m.GetByContractNr(1)).Returns(_dummyContext.ContractPiet1);
            _GebruikerRepository.Setup(m => m.DeleteContract(It.IsAny<Contract>()));
            var result = Assert.IsType<ViewResult>(_controller.Delete(1));
            Assert.Equal("ContractPiet1", result.ViewData["name"]);
        }
        #endregion

        #region -- Delete POST --
        [Fact]
        public void Delete_ExistingContract_DeletesContractAndPersistsChangesAndRedirectsToActionIndex()
        {
            _GebruikerRepository.Setup(m => m.GetByContractNr(1)).Returns(_dummyContext.ContractPiet1);
            _GebruikerRepository.Setup(m => m.DeleteContract(It.IsAny<Contract>()));
            var result = Assert.IsType<RedirectToActionResult>(_controller.DeleteConfirmed(1));
            Assert.Equal("Index", result.ActionName);
            _GebruikerRepository.Verify(m => m.GetByContractNr(1), Times.Once());
            _GebruikerRepository.Verify(m => m.DeleteContract(It.IsAny<Contract>()), Times.Once());
            _GebruikerRepository.Verify(m => m.SaveChanges(), Times.Once());
        }
        #endregion
    }
}
