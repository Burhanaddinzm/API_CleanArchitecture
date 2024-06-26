﻿namespace API.Application.Dtos.Categories;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Logo { get; set; } = null!;
    public int? ParentId { get; set; }
}
