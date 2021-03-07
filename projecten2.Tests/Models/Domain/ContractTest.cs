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
            Gebruiker peter = null;
            Gebruiker piet = null;
            _alpha.VoegTicketToe("ContractNul", 1, 1, startDate, "omschrijving test", "opmerkingen test", peter);
            _alpha.VoegTicketToe("ContractTwee", 2, 2, startDate, "omschrijving test2", "opmerkingen test2", piet);
        }

        public void VoegTicketToe_ContractMetTweeTickets_voegtTicketToe()
        {
            int aantalTicketsVoorhand = _alpha.NrOfTickets;
            Gebruiker jan = null;
            _alpha.VoegTicketToe("ContractDrie", 3, 3, startDate, "omschrijving test3", "opmerkingen test3", jan);
            Assert.Equal(aantalTicketsVoorhand + 1, _alpha.NrOfTickets);
        }
    }
}