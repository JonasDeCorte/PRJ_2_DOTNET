using System;
using System.Collections.Generic;

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

        public List<Bedrijf> Bedrijf { get; set; }
        public ICollection<Contract> Contracten { get; set; }
        #endregion

        #region Constructors
        public Klant()
        {

        }

        public Klant(int klantNummer, string gegevensContactPersonen, DateTime registratie)
        {
            KlantNummer = klantNummer;
            GegevensContactPersonen = gegevensContactPersonen;
            DatumRegistratie = registratie;

        }

        public Klant(int klantNummer, string voornaam, string naam, string email, bool status, string gegevensContactPersonen, DateTime registratie)
        {
            KlantNummer = klantNummer;
            Voornaam = voornaam;
            Naam = naam;
            Email = email;
            Status = status;
            GegevensContactPersonen = gegevensContactPersonen;
            DatumRegistratie = registratie;
        }
        #endregion

        #region Methods
        public void VoegContractToe(Contract contract)
        {
            Contracten.Add(contract);
        }
        #endregion

    }
}
