using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using projecten2.Data;
using projecten2.filter;
using projecten2.Models.Domain;
using projecten2.Utilities;

namespace projecten2.Areas.Identity.Pages.Streaming
{   
    public class BufferedMultipleFileUploadDbModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".txt", ".png", ".jpg", ".jpeg", ".gif" };
        private readonly IGebruikerRepository _gebruikerRepository;
        public BufferedMultipleFileUploadDbModel(ApplicationDbContext context,
            IConfiguration config, IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
            _context = context;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        [BindProperty]
        public BufferedMultipleFileUploadDb FileUpload { get; set; }

        public string Result { get; private set; }

        public void OnGet()
        {
        }
       
        public async Task<IActionResult> OnPostUploadAsync(Klant klant ,int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }
            // Perform an initial check to catch FileUpload class
            // attribute violations.
            if (!ModelState.IsValid)
            {
                Result = "Gelieve het formulier te corrigeren.";

                return Page();
            }

            foreach (var formFile in FileUpload.FormFiles)
            {
                var formFileContent =
                    await FileHelpers.ProcessFormFile<BufferedMultipleFileUploadDb>(
                        formFile, ModelState, _permittedExtensions,
                        _fileSizeLimit);

                // Perform a second check to catch ProcessFormFile method
                // violations. If any validation check fails, return to the
                // page.
                if (!ModelState.IsValid)
                {
                    Result = "Gelieve het formulier te corrigeren.";

                    return Page();
                }

                // **WARNING!**
                // In the following example, the file is saved without
                // scanning the file's contents. In most production
                // scenarios, an anti-virus/anti-malware scanner API
                // is used on the file before making the file available
                // for download or for use by other systems. 
                // For more information, see the topic that accompanies 
                // this sample.

                var file = new AppFile()
                {
                    Content = formFileContent,
                    UntrustedName = formFile.FileName,
                    Note = FileUpload.Note,
                    Size = formFile.Length,
                    UploadDT = DateTime.UtcNow,
                    ticket = _gebruikerRepository.GetByTicketNr(id.Value)           
                };
                _context.File.Add(file);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }

    public class BufferedMultipleFileUploadDb
    {
        [Required]
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; }

        [Display(Name = "Omschrijving")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }
}
