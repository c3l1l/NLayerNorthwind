using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
    public class CategoryServiceWithCaching : ICategoryService
    {
        private const string CacheCategoryKey = "categoryCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;
            if (!_memoryCache.TryGetValue(CacheCategoryKey,out _))
            {
                _memoryCache.Set(CacheCategoryKey, _repository.GetAll().ToList());
            }
        }

        public async Task<Category> AddAsync(Category entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoryAsync();
            return entity;
        }

      

        public async Task<IEnumerable<Category>> AddRangeAsync(IEnumerable<Category> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoryAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Category, bool>> expression)
        {
           return Task.FromResult(_memoryCache.Get<IEnumerable<Category>>(CacheCategoryKey).Any(expression.Compile()));
           // throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Category>>(CacheCategoryKey));
        }

        public Task<Category> GetByIdAsync(int id)
        {
            var category = _memoryCache.Get<List<Category>>(CacheCategoryKey).FirstOrDefault(x=>x.CategoryID==id);
            if (category==null)
            {
                throw new NotFoundException($"{typeof(Category).Name} ({id}) not found");
            }
            return Task.FromResult(category);
        }

        public Task<Category> GetSingleCategoryByIdWithProdyctsAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Category entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoryAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Category> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoryAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoryAsync();
        }

        public IQueryable<Category> Where(Expression<Func<Category, bool>> expression)
        {
            return _memoryCache.Get<List<Category>>(CacheCategoryKey).Where(expression.Compile()).AsQueryable();
        }

        private async Task CacheAllCategoryAsync()
        {
            _memoryCache.Set(CacheCategoryKey, await _repository.GetAll().ToListAsync());
        }
    }
}
