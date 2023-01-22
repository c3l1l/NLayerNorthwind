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
    public class CategoriesController : CustomBaseController
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
            //return Ok(categoriesDto);
            return CreateCustomActionResult(CustomResponseDto<List<CategoryDto>>.Success(200,categoriesDto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var categoryDto = _mapper.Map<CategoryDto>(await _categoryService.GetByIdAsync(id));
           // return Ok(_mapper.Map<CategoryDto>(category));
           return CreateCustomActionResult(CustomResponseDto<CategoryDto>.Success(200,categoryDto));
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromForm]CategoryAddDto categoryAddDto)
        {
            var fileByte=_fileService.FileConvertByteToDb(categoryAddDto.Picture);
            var category = _mapper.Map<Category>(categoryAddDto);
            category.Picture= fileByte;
            var newCategoryDto=_mapper.Map<CategoryDto>(await _categoryService.AddAsync(category));
            // return Ok(await _categoryService.AddAsync(category));
            //return Created("",category);
            return CreateCustomActionResult(CustomResponseDto<CategoryDto>.Success(201, newCategoryDto));

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]CategoryUpdateDto categoryUpdateDto)
        {
            var fileByte = _fileService.FileConvertByteToDb(categoryUpdateDto.Picture);
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.Picture = fileByte;
            await _categoryService.UpdateAsync(category);
            // return NoContent();
            return CreateCustomActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            await _categoryService.RemoveAsync(category);
            //return NoContent();
            return CreateCustomActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
