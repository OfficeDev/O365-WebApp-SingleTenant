using System.Web;
using System.Web.Mvc;

namespace O365_WebApp_SingleTenant
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
