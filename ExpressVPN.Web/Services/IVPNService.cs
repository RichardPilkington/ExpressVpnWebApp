using System.Collections.Generic;
using ExpressVPN.Model;

namespace ExpressVPN.Web.Services
{
    public interface IVPNService
    {
        List<Continent> GetContinents();
        void ChangeRegion(string region);
    }
}