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
        public void VoegContractToe_KlantMetTweeContracten_getAantalActieveContracten()
        {
            Assert.Equal(2, _klant.GetAantalActieveContracten());
        }
        [Fact]
        public void VoegContractToe_KlantMetTweeContracten_getAantalActieveTickets()
        {
            Assert.Equal(2, _klant.GetAantalActieveTickets());
        }

        [Fact]
        public void VoegContractToe_KlantMetTweeContracten_voegtContractToe()
        {
            Contract contractDrie = new Contract(ContractStatus.LOPEND, "Contract Piet 3", 2, new ContractType());
            _klant.VoegContractToe(contractDrie);
            Assert.Equal(3, _klant.GetAantalActieveContracten());
        }

       
        [Fact]
        public void BerekenGemiddeldAantalUrenTicket_TicketMet2Uren_berekenAantalUren()
        {
            
            Assert.Equal(2, _klant.GetGemiddeldAantalUurPerTicket());
        }

    }
}
