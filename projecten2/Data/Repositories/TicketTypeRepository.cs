using Microsoft.EntityFrameworkCore;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Repositories
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly DbSet<TicketType> _TicketTypes;

        public TicketTypeRepository(ApplicationDbContext dbContext)
        {
            _TicketTypes = dbContext.TicketTypes;
        }
        public IEnumerable<TicketType> GetAll()
        {
            return _TicketTypes.ToList();
        }

        public TicketType GetBy(int id)
        {
            return _TicketTypes.SingleOrDefault(l => l.id.Equals(id));
        }
    }
}
