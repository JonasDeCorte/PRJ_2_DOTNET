using projecten2.Models;
using projecten2.Models.Chart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using projecten2.filter;
using Microsoft.AspNetCore.Authorization;
using projecten2.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace projecten2.ViewComponents
{
    
    [ViewComponent(Name = "grafiekactievetickets")]
    public class GrafiekActieveTicketsViewComponent : ViewComponent
    {
        private readonly IGebruikerRepository _klantenRepo;

        public GrafiekActieveTicketsViewComponent(IGebruikerRepository klantenrepo)
        {
            _klantenRepo = klantenrepo;
        }
        
       // [ServiceFilter(typeof(KlantFilter))]
        // [Authorize]
        public IViewComponentResult Invoke()
        {
            // Ref: https://www.chartjs.org/docs/latest/

            Klant klant = (Klant)_klantenRepo.GetByEmail(HttpContext.User.Identity.Name);



            string[] dataLabels2 = new string[6];
            int[] data = { 0, 0, 0, 0, 0, 0 };
            DateTime date = DateTime.Today;
            DateTime date2;
            for(int i = 5; i >= 0; i--)
            {
                date2 = date.AddDays(-7);
                dataLabels2[i] = string.Format("{0:dd/MM/yy}", date2) + "-" + string.Format("{0:dd/MM/yy}", date);
                foreach(Contract c in klant.Contracten)
                        foreach(Ticket t in c.Tickets)
                        if (t.AanmaakDatum <= date   )
                            if(t.DatumAfgewerkt.Equals(null) || t.DatumAfgewerkt >=date2 )
                            data[i]+=1;
                    
                date = date2.AddDays(-1);
            }
            
            string[] backgroundColor = {
                        "rgba(204, 214, 12, 0.2)",
                        "rgba(204, 214, 12, 0.2)",
                        "rgba(204, 214, 12, 0.2)",
                        "rgba(204, 214, 12, 0.2)",
                        "rgba(204, 214, 12, 0.2)",
                        "rgba(204, 214, 12, 0.2)"
                        };
                            
            Dataset dataset = new Dataset { label = "Actieve tickets", data = data, borderWidth =1,backgroundColor = backgroundColor};
            Dataset[] datasets = { dataset };
            

            ChartJs Chart = new ChartJs
            {
                type = "line",
                responsive = true,
                data = new Models.Chart.Data
                {
                    labels = dataLabels2,
                    datasets =  datasets 
                    
                },

            };
            
           /* var chart = JsonConvert.DeserializeObject<ChartJs>(chartData);*/
            var chartModel = new ChartJsViewModel
            {
                Chart = Chart,
                ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    
                })
            };

            return View(chartModel);
        }
    }
}