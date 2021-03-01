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
    }
}
