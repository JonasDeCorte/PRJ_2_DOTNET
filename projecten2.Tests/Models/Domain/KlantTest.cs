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
            Ticket t = new Ticket();
            t.AanmaakDatum = new DateTime(2021, 1, 1, 12, 0, 0);
            t.DatumAfgewerkt = new DateTime(2021, 1, 1, 14, 0, 0);
            Ticket t2 = new Ticket();
            t2.AanmaakDatum = new DateTime(2021, 1, 1, 17, 0, 0);
            t2.DatumAfgewerkt = new DateTime(2021, 1, 1, 19, 0, 0);
            _pol.Contracten.First().VoegTicketToe(t);
            _pol.Contracten.First().VoegTicketToe(t2);
            Assert.Equal(2, _pol.GetGemiddeldAantalUurPerTicket());
        }

    }
}
