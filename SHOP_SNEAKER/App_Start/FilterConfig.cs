using System.Web;
using System.Web.Mvc;

namespace SHOP_SNEAKER
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}