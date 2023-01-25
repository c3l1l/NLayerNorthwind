using AutoMapper;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using NorthwindExample.Core.Repositories;
using NorthwindExample.Core.Services;
using NorthwindExample.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Service.Services
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductsWithCategoryDto>> GetProductsWithCategory()
        {
            var products=await _productRepository.GetProductsWithCategory();
            var productWithCategoryDto = _mapper.Map<List<ProductsWithCategoryDto>>(products);
            return productWithCategoryDto;
        }

        public Task<List<ProductWithCategoryAndSupplierDto>> GetProductsWithCategoryAndSupplier()
        {
            throw new NotImplementedException();
        }

        public Task<ProductWithCategoryAndSupplierDto> GetProductWithCategoryAndSupplier(int id)
        {
            throw new NotImplementedException();
        }
    }
}
