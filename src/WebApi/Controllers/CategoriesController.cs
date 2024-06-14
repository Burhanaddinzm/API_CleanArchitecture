using API.Application.Dtos.Categories;
using API.Application.Services;
using API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var result = await _categoryService.CreateCategoryAsync(createCategoryDto);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, DeleteCategoryDto categoryDeleteDto)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id, categoryDeleteDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (await _categoryService.GetCategoryByIdAsync(id) is CategoryDto category)
        {
            return Ok(category);
        }
        else return Problem(statusCode: 404, title: "Category not found!");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (await _categoryService.GetAllCategoriesAsync() is IEnumerable<Category> categories)
        {
            return Ok(categories);
        }
        else return Problem(statusCode: 404, title: "Categories not found!");
    }
}
