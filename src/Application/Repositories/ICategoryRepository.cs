using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Repositories;

public interface ICategoryRepository
{
    Task CreateAsync(Category entity);
    Task UpdateAsync(Category entity);
    Task DeleteAsync(int id);
    Task<Category> GetByIdAsync(int id);
    Task<List<Category>> GetAllAsync();
}
