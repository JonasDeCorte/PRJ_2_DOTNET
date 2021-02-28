using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class TicketType
    {
        public int id { get; set; }
        public string Omschrijving { get; set; }
        public string Naam { get; set; }
       
        public TicketType(string omschrijving, string naam)
        {
            this.Omschrijving = omschrijving;
            this.Naam = naam;
        }

    }
}
