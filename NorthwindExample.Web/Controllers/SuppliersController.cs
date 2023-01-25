using Microsoft.AspNetCore.Mvc;

namespace NorthwindExample.Web.Controllers
{
    public class SuppliersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
