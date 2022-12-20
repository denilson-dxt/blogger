using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories;
public class FileRepository:GenericRepository<Domain.File>,IFileRepository
{
    private readonly DataContext _context;

    public FileRepository(DataContext context):base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.File>> ListByParentId(string parentId)
    {
        if(parentId == "root")
            parentId = null;
        return await _context.Files.Where(f => f.ParentFolder.Id == parentId).ToListAsync();
    }
}
