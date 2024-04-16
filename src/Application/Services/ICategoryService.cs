using API.Application.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Services;

public interface ICategoryService
{
    Task<CreateCategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<UpdateCategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(int id, DeleteCategoryDto deleteCategoryDto);
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto> GetCategoryByIdAsync(int id);
}
