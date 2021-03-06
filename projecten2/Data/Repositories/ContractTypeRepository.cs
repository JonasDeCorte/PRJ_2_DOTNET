using Microsoft.EntityFrameworkCore;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Repositories
{
    public class ContractTypeRepository : IContractTypeRepository
    {
        private readonly DbSet<ContractType> _contractTypes;

        public ContractTypeRepository(ApplicationDbContext context)
        {
            _contractTypes = context.ContractTypes;

        }

        public IEnumerable<ContractType> GetAll()
        {
            return _contractTypes.ToList();
        }
    }
}
