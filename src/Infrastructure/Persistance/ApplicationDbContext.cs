using API.Application.Common.Interfaces;
using API.Domain.Common;
using API.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IHttpContextAccessor _accessor;
    public ApplicationDbContext(
        DbContextOptions options,
        IHttpContextAccessor accessor)
        : base(options)
    {
        _accessor = accessor;
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseAuditableEntity>();
        var currentUserName = _accessor.HttpContext?.User.Identity?.Name ?? "Anonymous";
        var currentIpAddress = _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUserName;
                    entry.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IP = currentIpAddress;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = currentUserName;
                    entry.Entity.ModifiedAt = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IP = currentIpAddress;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
