using System;
using System.Collections.Generic;

namespace projecten2.Models.Domain
{
    public class Contract
    {
        
        #region Fields

        #endregion

        #region Properties
        public int ContractNr { get; set; }
        public string ContractTitel { get; set; }
        public int Doorlooptijd { get; set; }
      //  public int ContractTypeId { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }

       // public int KlantNr { get; set; }
       // public Klant Klant { get; set; }
        public ContractStatus ContractStatus { get; set; }
        public List<Ticket> Tickets { get; set; }
        public int NrOfTickets => Tickets.Count;
        public ContractType ContractType { get; set; }
        #endregion

        #region Constructors
        public Contract()
        {
            EindDatum = StartDatum.AddYears(Doorlooptijd);
            Tickets = new List<Ticket>();
        }
        public Contract(int contractNr)
        {
            if ( contractNr < 5)
                throw new ArgumentException();
            ContractNr = contractNr;
           
        }
        public Contract(ContractStatus status, string titel, int doorlooptijd, ContractType contractType) 
        {
            this.ContractStatus = status;
            this.ContractTitel = titel;
            this.StartDatum = DateTime.Today;
            this.EindDatum = StartDatum.AddYears(doorlooptijd);
            this.Doorlooptijd = doorlooptijd;
            this.ContractType = contractType;
            this.Tickets = new List<Ticket>();      
        }

    
        #endregion

        #region Methods
        public void VoegTicketToe(Ticket ticket)
        {
            if(ticket != null)
            {
                Tickets.Add(ticket);
            }
           
        }
      

        public void StopzettenContract(Contract contract)
        {
            if (contract != null)
            {
                contract.ContractStatus = ContractStatus.BEËINDIGD;
            }
            
        }

        #endregion

    }
}
