using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Web.Services;

namespace NorthwindExample.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;
        private readonly SupplierApiService _supplierApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService, SupplierApiService supplierApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
            _supplierApiService = supplierApiService;
        }

        public async Task<IActionResult> Index()
        {
            GetCategoriesAndSuppliersWithSelectList();           
           // return View(await _productApiService.GetProductsWithCategoryAsync());
            return View(await _productApiService.GetProductsWithCategoryAndSupplierAsync());
        }
        public async Task<IActionResult> Save()
        {
            await GetCategoriesAndSuppliersWithSelectList();
            return View();
        }       

        [HttpPost]
        public async Task<IActionResult> Save(ProductAddDto productAddDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productAddDto);
                return RedirectToAction(nameof(Index));
            }
            await GetCategoriesAndSuppliersWithSelectList();
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            await GetCategoriesAndSuppliersWithSelectList();
          var product= await _productApiService.GetByIdAsync(id);
           //var product= await _productApiService.GetProductWithCategoryAndSupplierAsync(id);
            return View(product);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)            {
                
                await _productApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            await GetCategoriesAndSuppliersWithSelectList();
            return View(productDto);
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private async Task GetCategoriesAndSuppliersWithSelectList()
        {
            var categories = await _categoryApiService.GetAllAsync();
            var suppliers = await _supplierApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.suppliers = new SelectList(suppliers, "SupplierID", "CompanyName");
        }
    }
}
