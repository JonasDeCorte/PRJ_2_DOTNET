using projecten2.Tests.Data;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace projecten2.Tests.Models.Domain
{
    public class TicketTest
    {
        private DummyDbContext _context;
        private DateTime dag;

        #region Constructor
        public TicketTest()
        {
            _context = new DummyDbContext();
            dag = _context.Dag;
        }
        #endregion
        [Fact]
        public void BerekenAantalUrenTicket_TicketMet2Uren_berekenAantalUren()
        {
            Ticket t = new Ticket();
            t.AanmaakDatum = new DateTime(2021, 1, 1, 12, 0, 0);
            t.DatumAfgewerkt = new DateTime(2021, 1, 2, 12, 0, 0);
            Assert.Equal(24, t.berekenAantaluren());
        }
    }
}
