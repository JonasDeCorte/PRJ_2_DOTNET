using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.filter
{   
    
    public class KlantFilter : ActionFilterAttribute
    {
        private readonly IGebruikerRepository _klantenRepo;
        public KlantFilter(IGebruikerRepository klantenRepo)
            {
            _klantenRepo = klantenRepo;
        }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
            context.ActionArguments["klant"] = context.HttpContext.User.Identity.IsAuthenticated ? _klantenRepo.GetByEmail(context.HttpContext.User.Identity.Name ) : null;
            base.OnActionExecuting(context);
        }
    }
}
    
    

