using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistance;

namespace API.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
