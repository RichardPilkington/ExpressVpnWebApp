using ExpressVPN.Model;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExpressVPN.DataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new HtmlDocument();
            doc.Load("c:\\temp\\data.html");
            var continents = doc.DocumentNode.ChildNodes.First().ChildNodes.Where (s=>s.Name =="table");
            var ContObj = ProcessContinents(continents);
            File.WriteAllText("C:\\temp\\data.json", JsonConvert.SerializeObject(ContObj));
        }

        private static List<Continent> ProcessContinents(IEnumerable<HtmlNode> continents)
        {
            var result = new List<Continent>();
            foreach (var item in continents)
            {
                var name = item.SelectNodes("thead/tr/th/h4").First().InnerText;
                result.Add(new Continent() { Name = name.Trim(), Countries = GetCountries(item) });

            }
            return result;
        }

        private static List<Country> GetCountries(HtmlNode item)
        {
            var result = new List<Country>();
            var countries = item.SelectNodes("tbody/tr");
            foreach (var co in countries)
            {
                var name = co.Descendants("div");
                var coun=name.Where(s => s.Attributes.Any(a=>a.Name=="class"&& a.Value=="country-name"));
                if (!coun.Any())
                    continue;
                    var t= coun.First ().InnerText;
             
                var css = co.SelectNodes("td/div")?.First()?.ChildNodes[1].Attributes["class"].Value; ;
                
                result.Add(new Country() { Name = t?.Trim(), cssClass = css });

            }
            return result.Where (s=>s.Name !="").ToList();
        }
    }
}
