using API.Application.Common.Interfaces;
using API.Domain.Common;
using API.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    readonly IHttpContextAccessor _accessor;
    public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor accessor) : base(options)
    {
        _accessor = accessor;
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseAuditableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _accessor.HttpContext.User.Identity.Name ?? "newUser";
                    entry.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = _accessor.HttpContext.User.Identity!.Name!;
                    entry.Entity.ModifiedAt = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    break;
                default:
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
