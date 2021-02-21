using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class Contract
    {
        #region Fields

        #endregion

        #region Properties
        public int ContractNr { get; set; }
        public DateTime Doorlooptijd { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }

        public Klant Klant { get; set; }
        public ContractStatus ContractStatus { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ContractType ContractType {get; set;}  
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

    }
}
