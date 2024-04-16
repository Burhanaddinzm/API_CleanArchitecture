using API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Entities;

public class Category : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string Logo { get; set; } = null!;
    public int? ParentId { get; set; }
    public Category? Parent { get; set; }
    public ICollection<Category>? SubCategories { get; set; }
    public ICollection<Product>? Products { get; set; }
    public Category()
    {
        SubCategories = new HashSet<Category>();
    }
}
