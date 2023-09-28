using DEMOService.Configuration;
using MVCWeb.Models;

namespace MVCWeb.Services
{
    public class MSAService
    {
        AppConfiguration appConfiguraiton;
        public MSAService(AppConfiguration appConfiguraiton)
        {
            this.appConfiguraiton = appConfiguraiton;
        }
    }
}
