using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class AppFile
    {
        public int Id { get; set; }
        public Ticket ticket { get; set; }
        public byte[] Content { get; set; }

        [Display(Name = "File naam")]
        public string UntrustedName { get; set; }

        [Display(Name = "Omschrijving")]
        public string Note { get; set; }

        [Display(Name = "grootte")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public long Size { get; set; }

        [Display(Name = "Uploaded")]
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime UploadDT { get; set; }
    }
}
