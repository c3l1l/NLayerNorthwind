using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindExample.API.Filters;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using NorthwindExample.Core.Services;
using NorthwindExample.Service.Validations;

namespace NorthwindExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products=await _productService.GetAllAsync();
            var productsDto=_mapper.Map<List<ProductDto>>(products.ToList());            
            return CreateCustomActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var productDto = _mapper.Map<ProductDto>(await _productService.GetByIdAsync(id));
            return CreateCustomActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto);
            var productDto = _mapper.Map<ProductDto>(await _productService.AddAsync(product));
            return CreateCustomActionResult(CustomResponseDto<ProductDto>.Success(201, productDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(productDto));           
            return CreateCustomActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

  //      [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return CreateCustomActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var productsWithCategoryDtos = await _productService.GetProductsWithCategory();            
            return CreateCustomActionResult(CustomResponseDto<List<ProductsWithCategoryDto>>.Success(200, productsWithCategoryDtos));
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductWithCategoryAndSupplier(int id)
        {
            var product = await _productService.GetProductWithCategoryAndSupplier(id);
            return CreateCustomActionResult(CustomResponseDto<ProductWithCategoryAndSupplierDto>.Success(200, product));

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategoryAndSupplier()
        {
            var products = await _productService.GetProductsWithCategoryAndSupplier();
            return CreateCustomActionResult(CustomResponseDto<List<ProductWithCategoryAndSupplierDto>>.Success(200, products));

        }
    }
}
