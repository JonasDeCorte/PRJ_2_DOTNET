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
        public void NewContract_ValidData_CreatesContract()
        {
            Contract contract = new Contract(5);
            Assert.Equal(5, contract.ContractNr);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void NewContract_InvalidQuantity_ThrowsArgumentException(int contractNr)
        {
            Assert.Throws<ArgumentException>(() => new Contract( contractNr));
        }

        #endregion

        [Fact]
        #region VoegTicketToe
        public void VoegTicketToe_ContractMetTweeTickets_voegtTicketToe()
        {
            int aantalTicketsVoorhand = _alpha.NrOfTickets;
            Ticket ticketContractDrie = null;

            _alpha.VoegTicketToe(ticketContractDrie);

            Assert.Equal(aantalTicketsVoorhand + 1, _alpha.NrOfTickets);

            #endregion
        }
    }
}
