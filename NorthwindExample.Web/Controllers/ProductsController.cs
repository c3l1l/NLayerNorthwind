using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindExample.Web.Services;

namespace NorthwindExample.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            //ViewBag.categories = await _categoryApiService.GetAllAsync();
           var categories= await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View(await _productApiService.GetProductsWithCategoryAsync());
        }
        public async Task<IActionResult> Save()
        {
            //var categoriesDto=await .....
            return View();
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
