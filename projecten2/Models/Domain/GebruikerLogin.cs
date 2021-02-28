using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class GebruikerLogin
    {
        public int Id { get; set; }
        public DateTime Datum_TijdStip { get; set; }   
        public LoginResult LoginResult { get; set; }
        public string Username { get; set; }
    }
}
