using API.Application.Dtos.Categories;
using API.Application.Services;
using API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.WebApi.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
public class CategoriesController : ApiController
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
        else
        {
            errors.Add(new("Category not found!", StatusCodes.Status404NotFound));
            return Problem(errors);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (await _categoryService.GetAllCategoriesAsync() is IEnumerable<Category> categories)
        {
            return Ok(categories);
        }
        else
        {
            errors.Add(new("Categories not found!", StatusCodes.Status404NotFound));
            return Problem(errors);
        }
    }
}
