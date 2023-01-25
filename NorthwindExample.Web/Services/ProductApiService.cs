using NorthwindExample.Core.DTOs;

namespace NorthwindExample.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var response =await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductsWithCategoryDto>>>("products/GetProductsWithCategory");
            return response.Data;
        }
        public async Task<ProductWithCategoryAndSupplierDto> GetProductWithCategoryAndSupplierAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductWithCategoryAndSupplierDto>>($"products/GetProductWithCategoryAndSupplier/{id}");
            return response.Data;
        }
        public async Task<List<ProductWithCategoryAndSupplierDto>> GetProductsWithCategoryAndSupplierAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryAndSupplierDto>>>("products/GetProductsWithCategoryAndSupplier");
            return response.Data;
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");
            return response.Data;
        }
        public async Task<ProductDto> SaveAsync(ProductAddDto newProduct)
        {
            var response=await _httpClient.PostAsJsonAsync("products", newProduct);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            return responseBody.Data;
        }
        public async Task<bool> UpdateAsync(ProductDto productDto)
        {
            var response = await _httpClient.PutAsJsonAsync("products", productDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
