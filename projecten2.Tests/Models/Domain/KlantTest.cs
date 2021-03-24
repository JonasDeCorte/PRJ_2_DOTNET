using projecten2.Models.Domain;
using projecten2.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace projecten2.Tests.Models.Domain
{
   
    public class KlantTest
    {
        private readonly Klant _klant;
        

     
        public KlantTest()
        {
            DummyDbContext context = new DummyDbContext();
            _klant = context.Piet;
            
           

        }
        [Fact]
        public void GetAantalActieveContracten_KlantMetTweeContracten_GeeftAantalActieveContracten()
        {
            Assert.Equal(2, _klant.GetAantalActieveContracten());
        }
        [Fact]
        public void getAantalActieveTickets_KlantMetTweeContracten_GeeftAantalActieveTickets()
        {
            Assert.Equal(2, _klant.GetAantalActieveTickets());
        }
        [Fact]
        public void getAllActiveTicketsTrue_KlantMetDrieTickets_GeeftAlleTickets()
        {
            Assert.Equal(3, _klant.GetAllActiveTickets(true).Count);
        }
        [Fact]
        public void getAllActiveTicketsFalse_KlantMetTweeActieveTickets_GeeftAlleActieveTickets()
        {
            Assert.Equal(2, _klant.GetAllActiveTickets().Count);
        }
        [Fact]
        public void getAantalActiveTicketsByTicketType_KlantMetTweeActieveTicketsType1_GeeftAantalActieveTicketsPerTickettype()
        {
            Assert.Equal(2, _klant.getAantalActiveTicketsByTicketType("HIGHEST PRIORITY"));
        }
        [Fact]
        public void getAantalActiveTicketsByContractid_KlantMetTweeActieveTicketsContract1_GeeftAantalActieveTicketsPerContractId()
        {
            Assert.Equal(2, _klant.GetAllActiveTicketsByContractId(1).Count);
        }
        [Fact]
        public void BerekenGemiddeldAantalUrenTicket_TicketMet2Uren_berekenAantalUren()
        {
            
            Assert.Equal(2, _klant.GetGemiddeldAantalUurPerTicket());
        }
        [Fact]
        public void GetContractById_ContractMetId1_GeeftContractMetId1()
        {

            Assert.Equal("Contract Piet 1", _klant.GetContractById(1).ContractTitel);
        }
        [Fact]
        public void AddTicketByContractId_ContractMetId1_VoegtTicketToeBijContract_()
        {
            Ticket t = _klant.AddTicketByContractId(1, new Ticket());
            Assert.Equal(3, _klant.GetContractById(1).NrOfTickets);
        }
        [Fact]
        public void VoegContractToe_KlantMetTweeContracten_voegtContractToe()
        {
            Contract contractDrie = new Contract(ContractStatus.LOPEND, "Contract Piet 3", 2, new ContractType());
            _klant.VoegContractToe(contractDrie);
            Assert.Equal(3, _klant.GetAantalActieveContracten());
        }
    }
}
