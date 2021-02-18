using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class ContractType
    {
        #region Fields

        #endregion

        #region Properties
        public int ContractTypeId { get; set; }
        public string Naam { get; set; }
        public string Status { get; set; }
        public DateTime TijdstippenTicketAanmaken { get; set; }
        public int MaximaleAfhaaltijd { get; set; }
        public int MinimaleAfhaaltijd { get; set; }
        public double Prijs { get; set; }

        public ManierTicketAanmaken ManierTicketAanmaken { get; set; }
<<<<<<< Updated upstream
        public Contract Contracten { get; set; }
=======
        public ICollection<Contract> Contracten { get; set; }
>>>>>>> Stashed changes
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

    }
}
