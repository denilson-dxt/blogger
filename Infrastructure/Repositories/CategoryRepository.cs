using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context):base(context)
    {
        _context = context;
    }

    public async Task<Category> GetBySlug(string slug)
    {
        return await _context.Set<Category>().FirstOrDefaultAsync(c => c.Slug == slug);
    }
}