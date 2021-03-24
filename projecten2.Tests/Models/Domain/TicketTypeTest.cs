using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace projecten2.Tests.Models.Domain
{
    public class TicketTypeTest
    {
        public TicketType _ticketType;
        public TicketTypeTest()
        {

        }
        [Fact]
        public void NewTicketTypeSucces()
        {
            _ticketType = new TicketType();
            Assert.IsType<TicketType>(_ticketType);
        }
        [Fact]
        public void NewContractTypeFail()
        {
            _ticketType = null;
            Assert.IsNotType<TicketType>(_ticketType);
        }
        [Fact]
        public void NewContractTypeSuccesWithParameters()
        {
            _ticketType = new TicketType("PRODUCTIE_GEIMPACTEERD_BINNEN_2U_OPLOSSING", "Hoog"); 
            Assert.IsType<TicketType>(_ticketType);
            Assert.Equal("PRODUCTIE_GEIMPACTEERD_BINNEN_2U_OPLOSSING", _ticketType.Omschrijving);
            Assert.Equal("Hoog", _ticketType.Naam);
            
        }
        [Fact]
        public void NewContractTypeFailWithParameters()
        {
            Assert.Throws<ArgumentException>(() => new TicketType(string.Empty, string.Empty));
        }
        [Fact]
        public void NewContractType_Null_Fails()
        {
            Assert.Throws<ArgumentNullException>(() => new TicketType(null, null));
        }
    }
}
