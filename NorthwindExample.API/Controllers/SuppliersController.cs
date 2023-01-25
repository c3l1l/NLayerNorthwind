using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using NorthwindExample.Core.Repositories;
using NorthwindExample.Core.Services;
using NorthwindExample.Core.UnitOfWorks;

namespace NorthwindExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : CustomBaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SuppliersController( ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All() { 
        var suppliers=await _supplierService.GetAllAsync();
            var suppliersDto = _mapper.Map<List<SupplierDto>>(suppliers);
            return CreateCustomActionResult(CustomResponseDto<List<SupplierDto>>.Success(200,suppliersDto));
        }
    }
}
