using System.Web;
using System.Web.Mvc;

namespace GET_WELL_SOON_CLINIC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
