using System.Web;
using System.Web.Mvc;
using PortalDeCiclismo.App_Start;

namespace PortalDeCiclismo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ActionFilter());
        }
    }
}
