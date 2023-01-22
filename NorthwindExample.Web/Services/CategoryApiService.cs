using NorthwindExample.Core.DTOs;
using System.Net.Http;

namespace NorthwindExample.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;
        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");
            return response.Data;
        }
    //    public async Task<ProductDto> GetByIdAsync(int id)
    //    {
    //        var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");
    //        return response.Data;
    //    }
    //    public async Task<ProductDto> SaveAsync(ProductAddDto newProduct)
    //    {
    //        var response = await _httpClient.PostAsJsonAsync("products", newProduct);
    //        if (!response.IsSuccessStatusCode) return null;
    //        var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
    //        return responseBody.Data;
    //    }
    //    public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto)
    //    {
    //        var response = await _httpClient.PutAsJsonAsync("products", productUpdateDto);
    //        return response.IsSuccessStatusCode;
    //    }
    //    public async Task<bool> RemoveAsync(int id)
    //    {
    //        var response = await _httpClient.DeleteAsync($"products/{id}");
    //        return response.IsSuccessStatusCode;
    //    }
    }
}
