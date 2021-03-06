using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public interface ITicketTypeRepository
    {
        TicketType GetBy(int Id);
        IEnumerable<TicketType> GetAll();
    }
}
