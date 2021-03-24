using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class TicketType
    {
        public int id { get; set; }
        private string _omschrijving;
        public string Omschrijving {
            get { return _omschrijving; }

            set
            {
                if (value == string.Empty)

                    throw new ArgumentException(nameof(Omschrijving), "Omschrijving moet een waarde hebben");
                if (value == null)
                    throw new ArgumentNullException(nameof(Omschrijving));
                _omschrijving = value;
            }
        }      
        private string _naam;
        public string Naam
        {
            get { return _naam; }

            set
            {
                if (value == string.Empty)

                    throw new ArgumentException(nameof(Naam), "Naam moet een waarde hebben");
                if (value == null)
                    throw new ArgumentNullException(nameof(Naam));
                _naam = value;
            }
        }
        public TicketType(string omschrijving, string naam)
        {
            this.Omschrijving = omschrijving;
            this.Naam = naam;
        }

        public TicketType()
        {
        }
    }
}
