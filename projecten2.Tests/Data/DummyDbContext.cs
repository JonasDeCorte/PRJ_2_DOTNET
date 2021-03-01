using projecten2.Models.Domain;
using System;
using System.Collections.Generic;

namespace projecten2.Tests.Data
{
    public class DummyDbContext
    {
        // Actieve Klant met 2 Contracten met 1 en 2 Tickets
        public Klant Piet { get; }                  
        public Contract ContractPiet1 { get; }
        public Contract ContractPiet2 { get; }
        public Ticket TicketPiet1 { get; }
        public Ticket TicketPiet2 { get; }
        public Ticket TicketPiet3 { get; }
        // Inactieve Klant met 1 Contract met 1 Ticket
        public Klant Sam { get; }                   
        public Contract ContractSam1 { get; }
        public Ticket TicketSam1 { get; }
        public ICollection<Klant> Klanten { get; }

        public Bedrijf BEEGO { get; }       // Bedrijf van Klant Sam
        public Bedrijf Microsoft { get; }   // Bedrijf van Klant Piet
        public ICollection<Bedrijf> Bedrijven {get; }

        public DateTime Dag { get; }

        public DummyDbContext()
        {
            String[] nummersBEEGO = { "+32 517 47 89 65", "+32 586 44 88 66", "+32 886 54 89 63" };
            String[] nummersMicrosoft = { "001-934 4444 56" };
            BEEGO = new Bedrijf("BEEGO", nummersBEEGO, "Brussel", "Steenstraat 48");
            Microsoft = new Bedrijf("Microsoft", nummersMicrosoft, "New York", "45 Wall Street");
            Bedrijven = new[] { BEEGO, Microsoft };

            Piet = new Klant(2, "Piet", "Van Steen", "Piet.VanSteen@Microsoft.be", true, "Gegevens Piet", new DateTime(24 / 10 / 2021));
            Sam = new Klant(3, "Sam", "Ross", "Sam.Ross@BEEGO.be", false, "Gegevens Sam", new DateTime(03 / 11 / 2021));

            ContractPiet1 = new Contract("Contract Piet 1", 2, ContractStatus.LOPEND, new DateTime(24 / 10 / 2021), new ContractType());
            ContractPiet2 = new Contract("Contract Piet 2", 2,  ContractStatus.LOPEND, new DateTime(20 / 01 / 2022), new ContractType());
            TicketPiet1 = new Ticket("Titel ticketP1", 1, 1, new DateTime(26 / 11 / 2021), "Omschrijving ticketP1", "Opmerkingen ticketP1", Piet);
            TicketPiet2 = new Ticket("Titel ticketP2 ", 2, 1, new DateTime(13 / 12 / 2021), "Omschrijving ticketP2", "Opmerkingen ticketP2", Piet);
            TicketPiet3 = new Ticket("Titel ticketP3", 2, 2, new DateTime(12 / 02 / 2022), "Omschrijving ticketP3", "Opmerkingen ticketP3", Piet);
            ContractPiet1.VoegTicketToe(TicketPiet1);
            ContractPiet1.VoegTicketToe(TicketPiet2);
            ContractPiet2.VoegTicketToe(TicketPiet3);
            Piet.VoegContractToe(ContractPiet1);
            Piet.VoegContractToe(ContractPiet2);

            ContractSam1 = new Contract("Contract Sam",3, ContractStatus.BEËINDIGD, new DateTime(03 / 11 / 2021), new ContractType());
            TicketSam1 = new Ticket("Titel ticketS1", 1,1, new DateTime(24 / 12 / 2021), "Omschrijving ticketS1", "Opmerkingen ticketS1", Sam);
            ContractSam1.VoegTicketToe(TicketSam1);
            Sam.VoegContractToe(ContractSam1);

            Klanten = new[] {Piet, Sam };

            Dag = new DateTime(DateTime.Now.Year + 1,3,1);     // 01/03/2022
        }
    }
}
