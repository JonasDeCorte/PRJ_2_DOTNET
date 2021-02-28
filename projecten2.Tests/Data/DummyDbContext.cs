using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projecten2.Models.Domain;

namespace projecten2.Tests.Data
{
    class DummyDbContext
    {
        public Klant Jonas { get; }         // heeft 1 Contract met 2 Tickets
        public Klant Piet { get; }          // heeft 2 Contracten met 1 en 2 Tickets
        public Klant Kelly { get; }         // heeft 1 afgelopen Contract met 2 Tickets
        public Klant Sam { get; }           // Inactieve Klant
        public Bedrijf BEEGO { get; }       // Bedrijf van Klant Jonas
        public Bedrijf Microsoft { get; }   // Bedrijf van Klant Piet en Kelly

        public ICollection<Contract> Contracten { get; }
        public ICollection<Ticket> Tickets { get; }

        public DummyDbContext()
        {
            String[] nummersBEEGO = { "+32 517 47 89 65", "+32 586 44 88 66", "+32 886 54 89 63" };
            String[] nummersMicrosoft = { "001-934 4444 56" };
            BEEGO = new Bedrijf("BEEGO", nummersBEEGO, "Brussel", "Steenstraat 48");
            Microsoft = new Bedrijf("Microsoft", nummersMicrosoft, "New York", "45 Wall Street");

            Jonas = new Klant(1, "Jonas", "De Langhe", "Jonas.DeLanghe@BEEGO.be", true, "Gegevens Jonas", new DateTime(01 / 01 / 2020));
            Piet = new Klant(2, "Piet", "Van Steen", "Piet.VanSteen@Microsoft.be", true, "Gegevens Piet", new DateTime(24 / 10 / 2019));
            Kelly = new Klant(3, "Kelly", "Weyts", "Kelly.Weyts@Microsoft.be", true, "Gegevens Kelly", new DateTime(05 / 07 / 2018));
            Sam = new Klant(4, "Sam", "Ross", "Sam.Ross@BEEGO.be", false, "Gegevens Sam", new DateTime(03 / 04 / 2019));

            Contract contractJonas1 = new Contract(Jonas, ContractStatus.LOPEND, new DateTime(01 / 01 / 2020), new ContractType());
            Ticket ticketJonas1 = new Ticket("Titel ticketJ1", 1, new DateTime(03 / 12 / 2020), "Omschrijving ticketJ1", "Opmerkingen ticketJ1", Jonas, contractJonas1);
            Ticket ticketJonas2 = new Ticket("Titel ticketJ2", 2, new DateTime(17 / 02 / 2020), "Omschrijving ticketJ2", "Opmerkingen ticketJ2", Jonas, contractJonas1);

            Contract contractPiet1 = new Contract(Piet, ContractStatus.LOPEND, new DateTime(24 / 10 / 2019), new ContractType());
            Contract contractPiet2 = new Contract(Piet, ContractStatus.LOPEND, new DateTime(20 / 09 / 2020), new ContractType());
            Ticket ticketPiet1 = new Ticket("Titel ticketP1", 1, new DateTime(26 / 11 / 2019), "Omschrijving ticketP1", "Opmerkingen ticketP1", Piet, contractPiet1);
            Ticket ticketPiet2 = new Ticket("Titel ticketP2 ", 1, new DateTime(13 / 01 / 2020), "Omschrijving ticketP2", "Opmerkingen ticketP2", Piet, contractPiet1);
            Ticket ticketPiet3 = new Ticket("Titel ticketP3", 2, new DateTime(12 / 10 / 2020), "Omschrijving ticketP3", "Opmerkingen ticketP3", Piet, contractPiet2);

            Contract contractKelly1 = new Contract(Kelly, ContractStatus.BEËINDIGD, new DateTime(05 / 07 / 2018), new ContractType());
            Ticket ticketKelly1 = new Ticket("Titel ticketK1", 3, new DateTime(03 / 08 / 2018), "Omschrijving ticketK1", "Opmerkingen ticketK1", Kelly, contractKelly1);
            Ticket ticketKelly2 = new Ticket("Titel ticketK2", 2, new DateTime(12 / 10 / 2019), "Omschrijving ticketK2", "Opmerkingen ticketK2", Kelly, contractKelly1);

            Contract contractSam1 = new Contract(Sam, ContractStatus.BEËINDIGD, new DateTime(03 / 04 / 2019), new ContractType());
            Ticket ticketSam1 = new Ticket("Titel ticketS1", 1, new DateTime(24 / 04 / 2019), "Omschrijving ticketS1", "Opmerkingen ticketS1", Sam, contractSam1);
        }

    }
}
