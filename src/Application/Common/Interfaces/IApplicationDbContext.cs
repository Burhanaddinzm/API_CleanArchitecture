using Microsoft.EntityFrameworkCore;
using API.Domain.Entities;

namespace API.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
