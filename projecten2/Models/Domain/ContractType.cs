using System;
using System.Collections.Generic;

namespace projecten2.Models.Domain
{
    public class ContractType
    {
        #region Fields

        #endregion

        #region Properties
        public int ContractTypeId { get; set; }
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
        private string _status;
        public string Status
        {
            get { return _status; }

            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException(nameof(Status), "Status moet een waarde hebben");             
                }
                if(value==null)
                throw new ArgumentNullException(nameof(Status));
                _status = value;
            }
        }
        public DateTime TijdstippenTicketAanmaken { get; set; }
        public DateTime MaximaleAfhaaltijd { get; set; }
        public DateTime MinimaleAfhaaltijd { get; set; }
        private double _prijs;
        public double Prijs
        {
            get { return _prijs; }

            set
            {
                if (value == double.NaN)
                {
                    throw new ArgumentException(nameof(Prijs), "Prijs moet een waarde hebben");
                   
                }
                if (value <=0 ) throw new ArgumentOutOfRangeException(nameof(Prijs));
                _prijs = value;
            }
        }

        public ManierTicketAanmaken ManierTicketAanmaken { get; set; }
        public ICollection<Contract> Contracten { get; set; }
        #endregion

        #region Constructors
        public ContractType()
        {

        }
        public ContractType(string naam, string status, DateTime tijdstipaanmaken, DateTime maxAfhaaltijd, DateTime minAfhaaltijd, double prijs)
        {

            this.Naam = naam;
            this.Status = status;
            this.TijdstippenTicketAanmaken = tijdstipaanmaken;
            this.MaximaleAfhaaltijd = maxAfhaaltijd;
            this.MinimaleAfhaaltijd = minAfhaaltijd;
            this.Prijs = prijs;
        }
        #endregion

        #region Methods

        #endregion

    }
}
