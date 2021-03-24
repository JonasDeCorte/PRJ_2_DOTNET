using System;
using System.Collections.Generic;
using System.Linq;

namespace projecten2.Models.Domain
{
    public class Klant : Gebruiker
    {
        #region Fields

        #endregion

        #region Properties
        public int KlantNummer { get; set; }
        public string GegevensContactPersonen { get; set; }
        public DateTime DatumRegistratie { get; set; }

     
        public List<Bedrijf> bedrijven { get; set; }
       
        #endregion

        #region Constructors
        public Klant()
        {
            bedrijven = new List<Bedrijf>();
            Contracten = new List<Contract>();
            Tickets = new List<Ticket>();
            DatumRegistratie = DateTime.Now;
        }

        #endregion

        #region Methods

        public int GetAantalActieveContracten()
        {
            return Contracten.Count(x => x.ContractStatus.Equals(ContractStatus.LOPEND));
        }
        public int GetAantalActieveTickets()
        {
            return GetAllActiveTickets().Count();
        }
        public List<Contract> GetContracten()
        {
            return Contracten;
        }
        public List<Ticket> GetAllActiveTickets(bool status = false)
        {
            if (status)        
                return Contracten.SelectMany(x => x.Tickets).ToList();
            else      
                return Contracten.SelectMany(x => x.Tickets).Where(x => x.IsTicketStatus(TicketStatus.AANGEMAAKT) || x.IsTicketStatus(TicketStatus.INBEHANDELING)).ToList();
            
        }
        public int getAantalActiveTicketsByTicketType(string ticketType)
        {
            return GetAllActiveTickets().Where(t => t.TicketType.Naam == ticketType).Count();
            
        }
        public double GetGemiddeldAantalUurPerTicket()
        {
            double aantalUren = 0;
            double aantalTickets = 0;
            foreach (Ticket t in GetAllActiveTickets(true))
            {
                aantalUren += t.berekenAantaluren();
                aantalTickets += 1;
            }    
            return aantalUren/ aantalTickets;
        }
      
        public List<Ticket> GetAllActiveTicketsByContractId(int contractId, bool status = false)
        {
            if (status)
            return Contracten.Where(x => x.ContractNr.Equals(contractId)).SelectMany(x => x.Tickets).ToList();
            else   
            return Contracten.Where(x => x.ContractNr.Equals(contractId)).SelectMany(x => x.Tickets).Where(x => x.IsTicketStatus(TicketStatus.AANGEMAAKT) || x.IsTicketStatus(TicketStatus.INBEHANDELING)).ToList(); 
        }

        public Contract GetContractById(int id)
        {
            return Contracten.FirstOrDefault(x => x.ContractNr.Equals(id));
        }

        public Ticket AddTicketByContractId(int contractId, Ticket ticket)
        {
            Contract contract = Contracten.FirstOrDefault(x => x.ContractNr.Equals(contractId));
            if(ticket != null)
            {
                contract.VoegTicketToe(ticket);
                VoegTicketToe(ticket);
            }
            return ticket;
        }
         
        public void VoegTicketToe(Ticket ticket)
        {
            if(ticket != null)
            {
                 Tickets.Add(ticket);
            }
           
        }
        public void VoegContractToe(Contract contract)
        {
            if(contract != null)
            {              
                Contracten.Add(contract);
            }         
        }      
        #endregion

    }
}
