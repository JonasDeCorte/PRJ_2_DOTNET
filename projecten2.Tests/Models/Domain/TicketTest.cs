
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
        private readonly Ticket _ticket;


        public TicketTest()
        {
            _context = new DummyDbContext();
            dag = _context.Dag;
            _ticket = new Ticket("ticket1");

        }
        #region Constructor

        [Fact]
        public void NewTicket_CorrectName_CreatesTicket()
        {
            Ticket tickett = new Ticket("Ticket2");
            Assert.Equal("Ticket2", tickett.Titel);
            Assert.Equal(0, tickett.NrOfBijlages);
            Assert.Equal(TicketStatus.AANGEMAAKT, tickett.TicketStatus);
        }

        [Fact]
        public void NewTicket_AllAttributes_CreatesBrewer()
        {
            Gebruiker tom = new Klant();
            TicketType tickettype = new TicketType("type ticket", "TicketType");
            Ticket ticket2 = new Ticket(tom, "ticket2", "dit is ticket 2", "opmerking ticket 2", tickettype, dag, dag);
            Assert.Equal("ticket2", ticket2.Titel);
            Assert.Equal("dit is ticket 2", ticket2.Omschrijving);
            Assert.Equal(dag, ticket2.AanmaakDatum);
            Assert.Equal(dag, ticket2.DatumAfgewerkt);
            Assert.Equal(0, ticket2.NrOfBijlages);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void NewTicket_TitelIncorrect_ThrowsException(string titel)
        {
            Assert.Throws<ArgumentException>(() => new Ticket(titel));
        }


        #endregion

        #region
        [Fact]
        public void BerekenAantalUrenTicket_TicketMet2Uren_berekenAantalUren()
        {
            Ticket t = new Ticket();
            t.AanmaakDatum = new DateTime(2021, 1, 1, 12, 0, 0);
            t.DatumAfgewerkt = new DateTime(2021, 1, 2, 12, 0, 0);
            Assert.Equal(24, t.berekenAantaluren());
        }
        [Fact]
        public void AnnuleerEenTicket_AangemaaktTicket_annulerenTicket()
        {
            DateTime datum = DateTime.Today;
            Ticket tt = new Ticket();
            tt.AnnulerenTicket(tt);
            Assert.Equal(TicketStatus.GEANNULEERD, tt.TicketStatus);
            Assert.Equal(tt.DatumAfgewerkt, datum);
            Assert.Equal(tt.LaatstGewijzigd, datum);
        }
        [Fact]
        public void TicketStatusControleren_AangemaaktTicket_IsTicketStatus()
        {
            Ticket ttt = new Ticket();
            Assert.True(ttt.IsTicketStatus(TicketStatus.AANGEMAAKT));
        }


        #endregion

    }
}