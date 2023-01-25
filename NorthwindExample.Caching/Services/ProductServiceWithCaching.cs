using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Caching.Memory;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using NorthwindExample.Core.Repositories;
using NorthwindExample.Core.Services;
using NorthwindExample.Core.UnitOfWorks;
using NorthwindExample.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Caching.Services
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IProductRepository repository, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            if (!_memoryCache.TryGetValue(CacheProductKey,out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {
          var product=_memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x=>x.ProductID ==id);
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name} ({id}) not found");
            }
            return Task.FromResult(product);
        }

        public Task<List<ProductsWithCategoryDto>> GetProductsWithCategory()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var productsWithCategoryDto=_mapper.Map<List<ProductsWithCategoryDto>>(products);
            return Task.FromResult(productsWithCategoryDto);
        }
        public async Task<ProductWithCategoryAndSupplierDto> GetProductWithCategoryAndSupplier(int id)
        {
            var product =await _repository.GetProductWithCategoryAndSupplier(id);
                
            var productWithCategoryAndSupplierDto=_mapper.Map<ProductWithCategoryAndSupplierDto>(product);
            return productWithCategoryAndSupplierDto;
        }

        public async Task RemoveAsync(Product entity)
        {
             _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync(); 
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
           return  _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllProductsAsync()
        {
            // _memoryCache.Set(CacheProductKey, await _repository.GetProductsWithCategory().Result);
            _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
        }

        public async Task<List<ProductWithCategoryAndSupplierDto>> GetProductsWithCategoryAndSupplier()
        {
            var products = await _repository.GetProductsWithCategoryAndSupplier();

            var productsWithCategoryAndSupplierDto = _mapper.Map<List<ProductWithCategoryAndSupplierDto>>(products);
            return productsWithCategoryAndSupplierDto;
        }
    }
    
}
