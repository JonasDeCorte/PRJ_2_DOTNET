using Microsoft.EntityFrameworkCore;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Contract> _contracten;
        public ContractRepository(ApplicationDbContext context)
        {
            _context = context;
            _contracten = _context.Contracten;
        }
        public void Add(Contract contract)
        {
            _contracten.Add(contract);
        }
        public IEnumerable<Contract> GetAll()
        {
            return _contracten.Include(x => x.Tickets).ToList();
        }

       

        public Contract GetByContractNr(int contractNr)
        {
            return _contracten.Include(x => x.Tickets).FirstOrDefault(x => x.ContractNr == contractNr);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
