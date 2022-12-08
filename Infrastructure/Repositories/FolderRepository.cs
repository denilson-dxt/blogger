using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories;
public class FolderRepository:GenericRepository<Domain.Folder>,IFolderRepository
{
    private readonly DataContext _context;

    public FolderRepository(DataContext context):base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Folder>> GetByParentId(string parentId)
    {
        var folders = await _context.Folders.Where(f=>f.ParentId == parentId).ToListAsync();
        return folders;
    }
}