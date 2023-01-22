using Microsoft.AspNetCore.Mvc;
using NorthwindExample.Web.Services;

namespace NorthwindExample.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryApiService _categoryApiService;

        public CategoriesController(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryApiService.GetAllAsync());
            //return View();
        }
    }
}
