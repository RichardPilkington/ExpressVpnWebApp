using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpressVPN.Web.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ExpressVPN.Web.Services;

namespace ExpressVPN.Web.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IVPNService dataservice;

        public HomeController( IVPNService dataservice)
        {
           
            this.dataservice = dataservice;
        }
        public IActionResult Index()
        {



            List<Model.Continent> list = dataservice.GetContinents();
            return base.View(new IndexViewModel() { Continents = list, TopResults=list.SelectMany( s=>s.Countries).OrderByDescending (a=>a.Count).Take(6) });
        }
     
        public IActionResult Change(string region)
        {
            dataservice.ChangeRegion(region);
            return Redirect("/home");
        }

       
        
    }
}
