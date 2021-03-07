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
            _pol.VoegContractToe("ContractNul");
            _pol.VoegContractToe("ContractTwee");
        }

        public void VoegContractToe_KlantMetTweeContracten_voegtContractToe()
        {
            int aantalContractenVoorhand = _pol.GetAantalActieveContracten();
            _pol.VoegContractToe("ContractDrie");
            Assert.Equal(aantalContractenVoorhand + 1, _pol.GetAantalActieveContracten());
        }
    }
}
