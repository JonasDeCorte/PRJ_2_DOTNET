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
        /* 
           PRODUCTIE_GEIMPACTEERD_BINNEN_2U_OPLOSSING,
        PRODUCTIE_ZAL_STIL_VALLEN_BINNEN_4U_OPLOSSING,
        GEEN_PRODUCTIE_IMPACT_BINNEN_3DAGEN_ANTWOORD
         */
        public TicketType(string omschrijving, string naam)
        {
            this.Omschrijving = omschrijving;
            this.Naam = naam;
        }

    }
}
