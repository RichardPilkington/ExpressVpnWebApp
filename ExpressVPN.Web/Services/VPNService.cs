using ExpressVPN.Model;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressVPN.Web.Services
{
    public class VPNService : IVPNService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public VPNService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
           
        }

        public void ChangeRegion(string region)
        {
            ExecuteCommand("expressvpn", "disconnect");
            if(!string.IsNullOrEmpty(region))
            ExecuteCommand("expressvpn", $"connect \"{ region}\"");

            var data = GetContinents();
            foreach (var item in data)
            {
                foreach (var country in item.Countries)
                {
                    if(country.Name== region)
                    {
                        country.Count += 1;
                    }
                }
            }
            System.IO.File.WriteAllText(Path.Combine(_hostingEnvironment.WebRootPath, "data.json"),JsonConvert.SerializeObject(data));


        }

        private void ExecuteCommand(string v1, string v2)
        {
            var escapedArgs = "";
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = v1,
                    Arguments = v2,
                   
                }
            };
              process.Start();
               process.WaitForExit();
        }

        public List<Continent> GetContinents()
        {
            var text = System.IO.File.ReadAllText(Path.Combine(_hostingEnvironment.WebRootPath, "data.json"));
            return JsonConvert.DeserializeObject<List<Continent>>(text);
        }
    }
}
