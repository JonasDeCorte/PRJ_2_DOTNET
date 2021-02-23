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
        public Contract()
        {

        }
        public Contract(Klant klant, ContractStatus contractStatus, DateTime StartDatum, ContractType type)
        {
            this.Klant = klant;
            this.ContractStatus = contractStatus;
            this.StartDatum = StartDatum;
            this.ContractType = type;
        }
        #endregion

        #region Methods

        #endregion

    }
}
