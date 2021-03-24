using projecten2.Models.Domain;
using System;
using System.Collections.Generic;

namespace projecten2.Tests.Data
{
    public class DummyDbContext
    {
        // Klant met 2 Contracten met 1 en 2 Tickets
        public Klant Piet { get; }
        public Contract ContractPiet1 { get; }
        public Contract ContractPiet2 { get; }
        public Ticket TicketPiet1 { get; }
        public Ticket TicketPiet2 { get; }
        public Ticket TicketPiet3 { get; }

        // Klant met 1 Contract met 1 Ticket
        public Klant Sam { get; }
        public Contract ContractSam1 { get; }
        public Ticket TicketSam1 { get; }

        public ICollection<Klant> Klanten { get; }
        public ICollection<Ticket> Tickets { get; }
        public ICollection<Contract> Contracten { get; }
        public ICollection<Bedrijf> Bedrijven { get; }

        public DateTime Dag { get; }

        public ICollection<TicketType> TicketTypes { get; }
        public ICollection<ContractType> ContractTypes { get; }

        public DummyDbContext()
        {
            // Bedrijven
            String[] nummersBEEGO = { "+32 517 47 89 65", "+32 586 44 88 66", "+32 886 54 89 63" };
            String[] nummersMicrosoft = { "001-934 4444 56" };
            Bedrijf BEEGO = new Bedrijf("BEEGO", nummersBEEGO, "Brussel", "Steenstraat 48");
            Bedrijf Microsoft = new Bedrijf("Microsoft", nummersMicrosoft, "New York", "45 Wall Street");
            Bedrijven = new[] { BEEGO, Microsoft };

            // TicketTypes
            TicketType TicketType1 = new TicketType("PRODUCTIE_GEIMPACTEERD_BINNEN_2U_OPLOSSING", "HIGHEST PRIORITY");
            TicketType TicketType2 = new TicketType("PRODUCTIE_ZAL_STIL_VALLEN_BINNEN_4U_OPLOSSING", "MEDIUM PRIORITY");
            TicketType TicketType3 = new TicketType("GEEN_PRODUCTIE_IMPACT_BINNEN_3DAGEN_ANTWOORD", "LOW PRIORITY");
            TicketTypes = new[] { TicketType1, TicketType2, TicketType3 };

            // ContractTypes
            ContractType ContractType1 = new ContractType();
            ContractType ContractType2 = new ContractType();
            ContractTypes = new[] { ContractType1, ContractType2 };

            // Klanten
            Piet = new Klant();
            Sam = new Klant();

            // Contracten en Tickets van Piet
            ContractPiet1 = new Contract(ContractStatus.LOPEND, "Contract Piet 1", 1, ContractType1);
            ContractPiet1.ContractNr = 1;
            ContractPiet2 = new Contract(ContractStatus.LOPEND, "Contract Piet 2", 3, ContractType2);
            ContractPiet2.ContractNr = 2;
            Piet.VoegContractToe(ContractPiet1);
            Piet.VoegContractToe(ContractPiet2);

            Ticket t = new Ticket(Piet, "Ticket1", "Omschrijving ticket 1", "Opmerkingen ticket 1", TicketType1, DateTime.Now);
            t.AanmaakDatum = new DateTime(2021, 1, 1, 12, 0, 0);
            t.DatumAfgewerkt = new DateTime(2021, 1, 1, 14, 0, 0);
            Ticket t2 = new Ticket(Piet, "Ticket2", "Omschrijving ticket 2", "Opmerkingen ticket 2", TicketType1, DateTime.Now);
            t2.AanmaakDatum = new DateTime(2021, 1, 1, 17, 0, 0);
            t2.DatumAfgewerkt = new DateTime(2021, 1, 1, 19, 0, 0);
            Ticket t3 = new Ticket(Piet, "Ticket3", "Omschrijving ticket 3", "Opmerkingen ticket 3", TicketType3, DateTime.Now);
            t3.AanmaakDatum = new DateTime(2021, 1, 1, 19, 0, 0);
            t3.DatumAfgewerkt = new DateTime(2021, 1, 1, 21, 0, 0);
            t3.TicketStatus = TicketStatus.AFGEHANDELD;

            ContractPiet1.VoegTicketToe(t);
            ContractPiet1.VoegTicketToe(t2);
            ContractPiet2.VoegTicketToe(t3);

            // Contract en Ticket van Sam
            ContractSam1 = new Contract(ContractStatus.LOPEND, "Contract Sam 1", 1, ContractType1);
            Sam.VoegContractToe(ContractSam1);
            ContractSam1.VoegTicketToe(new Ticket(Sam, "Ticket4", "Omschrijving ticket 4", "Opmerkingen ticket 4", TicketType2, DateTime.Now));

            Klanten = new[] { Piet, Sam };
            Tickets = new[] { TicketPiet1, TicketPiet2, TicketPiet3, TicketSam1 };
            Contracten = new[] { ContractPiet1, ContractPiet2, ContractSam1 };

            Dag = new DateTime(DateTime.Now.Year + 1, 3, 1);     // 01/03/2022
        }
    }
}
