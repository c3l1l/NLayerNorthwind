using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using NorthwindExample.Core.Services;

namespace NorthwindExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, IFileService fileService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _fileService = fileService;
        }
        [HttpGet]
        public async Task<IActionResult> All() {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoriesDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var category = await _categoryService.GetByIdAsync(id);

            return Ok(_mapper.Map<CategoryDto>(category));
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromForm]CategoryAddDto categoryAddDto)
        {
            var fileByte=_fileService.FileConvertByteToDb(categoryAddDto.Picture);
            var category = _mapper.Map<Category>(categoryAddDto);
            category.Picture= fileByte;          
            return Ok(await _categoryService.AddAsync(category));            
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]CategoryUpdateDto categoryUpdateDto)
        {
            var fileByte = _fileService.FileConvertByteToDb(categoryUpdateDto.Picture);
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.Picture = fileByte;
            await _categoryService.UpdateAsync(category);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            await _categoryService.RemoveAsync(category);
            return NoContent();
        }
    }
}
