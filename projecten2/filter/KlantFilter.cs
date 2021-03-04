using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.filter
{   
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class KlantFilter : ActionFilterAttribute
    {
        private readonly UserManager<Gebruiker> _userManager;
        public KlantFilter(UserManager<Gebruiker> userManager)
            {
            _userManager = userManager;
        }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
            var user =  _userManager.FindByNameAsync(context.HttpContext.User.Identity.Name);
            context.ActionArguments["klant"] = context.HttpContext.User.Identity.IsAuthenticated ? user : null;
                base.OnActionExecuting(context);
            }
    }
}
    
    

