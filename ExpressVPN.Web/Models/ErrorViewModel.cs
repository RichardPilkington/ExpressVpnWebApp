using ExpressVPN.Model;
using System;
using System.Collections.Generic;

namespace ExpressVPN.Web.Models
{
    public class IndexViewModel
    {
       public List<Continent> Continents { get; set; }
        public IEnumerable<Country> TopResults { get; internal set; }
    }
}