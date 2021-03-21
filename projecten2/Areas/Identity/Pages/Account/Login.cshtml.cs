using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using projecten2.Models.Domain;
using projecten2.Data;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace projecten2.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _dbContext;
        private readonly INotyfService _notyf;
        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender,
            ApplicationDbContext context,
            INotyfService notyf)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _dbContext = context ;
            _notyf = notyf;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }        
        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required ]
            public string Gebruikersnaam { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Wachtwoord { get; set; }

            [Display(Name = "Wachtwoord onthouden?")]
            public bool Wachtwoord_onthouden { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var currentUser = await _userManager.FindByEmailAsync(Input.Gebruikersnaam);
                if (currentUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Geen geldidge inloggegevens.");
                    return Page();
                }

                var result = await _signInManager.PasswordSignInAsync(currentUser, Input.Wachtwoord, Input.Wachtwoord_onthouden, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User ingelogd.");
                    _notyf.Success("Logged in succesful", 3);
                    _dbContext.GebruikerLogins.Add(new GebruikerLogin { Datum_TijdStip = DateTime.UtcNow, LoginResult = LoginResult.GELUKT, Username = currentUser.Email });
                    _dbContext.SaveChanges();
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.Wachtwoord_onthouden });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Account geblokkeerd");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    _dbContext.GebruikerLogins.Add(new GebruikerLogin { Datum_TijdStip = DateTime.UtcNow, LoginResult = LoginResult.MISLUKT, Username = currentUser.Email });
                    _dbContext.SaveChanges();
                    ModelState.AddModelError(string.Empty, "Geen geldige inloggegevens.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Gebruikersnaam);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId, code },
                protocol: Request.Scheme);
            /*await _emailSender.SendEmailAsync(
                Input.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            */
            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return Page();
        }
    }
}
