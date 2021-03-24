using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace projecten2.Tests.Models.Domain
{
    public class ContractTest
    {
        public readonly Contract _alpha;
        DateTime startDate = new DateTime(2020, 9, 1, 13, 50, 25);



        public ContractTest()
        {
            _alpha = new Contract();
           
            Ticket ticketContractÉen = null;
            Ticket ticketContractTwee = null;

            _alpha.VoegTicketToe(ticketContractÉen);
            _alpha.VoegTicketToe(ticketContractTwee);
        }

        #region Constructor

        [Fact]
        public void NewContract_CorrectName_CreatesContract()
        {
            ContractStatus contractStatus = new ContractStatus();
            ContractType contractType = new ContractType();
            Contract contract = new Contract(contractStatus, "Conctract1", 10, contractType);
            Assert.Equal("Conctract1", contract.ContractTitel);
            Assert.Equal(10, contract.Doorlooptijd);
        }

      

        #endregion


        [Fact]
        #region methodes
        public void VoegTicketToe_ContractMetNulTickets_voegtTicketToe()
        {
            Contract contract = new Contract();
            Ticket ticket = new Ticket();
            int aantalTicketsVoorhand = contract.NrOfTickets;
           
            contract.VoegTicketToe(ticket);

            Assert.Equal(aantalTicketsVoorhand + 1, contract.NrOfTickets);

            
        }

        [Fact]
        public void ZetContractStop_AangemaaktContract_StopzettenContract()
        {
            DateTime datum = DateTime.Today;
            Contract cc = new Contract();
            cc.StopzettenContract(cc);
            Assert.Equal(ContractStatus.BEËINDIGD, cc.ContractStatus);
        }
        #endregion
    }
}
