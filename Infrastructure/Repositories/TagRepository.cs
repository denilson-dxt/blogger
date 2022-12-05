using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Persistence;

namespace Infrastructure.Repositories;
public class TagRepository:GenericRepository<Tag>,ITagRepository
{
    private readonly DataContext _context;

    public TagRepository(DataContext context):base(context)
    {
        _context = context;
    }

}