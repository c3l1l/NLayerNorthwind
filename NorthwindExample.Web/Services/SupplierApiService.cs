using NorthwindExample.Core.DTOs;

namespace NorthwindExample.Web.Services
{
    public class SupplierApiService
    {
        private readonly HttpClient _httpClient;

        public SupplierApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<SupplierDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<SupplierDto>>>("suppliers");
            return response.Data;
        }
    }
}
