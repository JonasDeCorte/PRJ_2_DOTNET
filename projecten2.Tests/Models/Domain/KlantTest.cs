/*using projecten2.Models.Domain;
using projecten2.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projecten2.Tests.Models.Domain
{
    public class KlantTest
    {
        private DummyDbContext _context;
        private Klant klantZonderRapport;
        private Klant klantMetTweeRapporten;

        public KlantTest()
        {
            _context = new DummyDbContext();
            klantZonderRapport = _context.NaamKlantZonderRapporten; //die namen moeten lik werken , bij beerhall is da piet er pieter
            klantMetTweeRapporten = _context.NaamKlantTweeRapporten;
        }

        public void VoegContractToe_KlantZonderRapport_voegtContractToe()
        {
            int aantalContracten = klantZonderRapport.NrOfBeers;
            _bockor.AddBeer("HoGent beer", 55.0D);
            Assert.Equal(nrOfBeersBeforeAdd + 1, _bockor.NrOfBeers);
        }
    }
}
*/


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
        private readonly Klant _pol;
        

     
        public KlantTest()
        {
            _pol = new Klant();
            Contract contractÉen = new Contract(ContractStatus.LOPEND, "ContractÉen",  2,new ContractType());
            Contract contractTwee = new Contract(ContractStatus.LOPEND, "ContractTwee", 2, new ContractType());
            _pol.VoegContractToe(contractÉen);
            _pol.VoegContractToe(contractTwee);


        }
        [Fact]
        public void VoegContractToe_KlantMetTweeContracten_voegtContractToe()
        {
            int aantalContractenVoorhand = _pol.GetAantalActieveContracten();
            Contract contractDrie = new Contract(ContractStatus.LOPEND, "ContractDrie", 2, new ContractType());
            _pol.VoegContractToe(contractDrie);
            Assert.Equal(aantalContractenVoorhand + 1, _pol.GetAantalActieveContracten());
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
