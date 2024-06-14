using API.Application.Dtos.Categories;
using API.Application.Repositories;
using API.Application.Services;
using API.Domain.Entities;
using API.Infrastructure.Extentions.File;
using AutoMapper;
using Microsoft.Extensions.Hosting;

namespace API.Infrastructure.Services;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHostEnvironment _environment;
    private readonly IMapper _mapper;
    public CategoryManager(
        ICategoryRepository categoryRepository,
        IHostEnvironment environment,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _environment = environment;
        _mapper = mapper;
    }

    public async Task<CreateCategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {

        var filename = await createCategoryDto.Logo.SaveFileAsync(_environment.ContentRootPath, "wwwroot/uploads");
        Category category = _mapper.Map<Category>(createCategoryDto);
        category.Logo = filename;
        await _categoryRepository.CreateAsync(category);

        return createCategoryDto;
    }

    public async Task DeleteCategoryAsync(int id, DeleteCategoryDto deleteCategoryDto)
    {
        if (id != deleteCategoryDto.Id) throw new Exception("Id is incorrect");
        await _categoryRepository.DeleteAsync(deleteCategoryDto.Id);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        IEnumerable<Category>? categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        Category? category = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<UpdateCategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
    {
        if (id != updateCategoryDto.Id) throw new Exception("Id is incorrect");
        var existCategory = await _categoryRepository.GetByIdAsync(id);
        var filename = await updateCategoryDto.Logo.SaveFileAsync(_environment.ContentRootPath, "wwwroot/uploads");
        updateCategoryDto.Logo.DeleteFile(_environment.ContentRootPath, "wwwroot/uploads", existCategory.Logo);
        Category category = _mapper.Map<Category>(updateCategoryDto);
        category.Logo = filename;
        await _categoryRepository.UpdateAsync(category);
        return updateCategoryDto;
    }
}
