using System.Web;
using System.Web.Mvc;

namespace Cumulative_N01652955
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
