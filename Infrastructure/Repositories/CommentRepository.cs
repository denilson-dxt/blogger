using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories;
public class CommentRepository: GenericRepository<Comment>, ICommentRepository
{
    private readonly DataContext _context;

    public CommentRepository(DataContext context):base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Comment>> GetByPostId(string postId)
    {
        var comments = await _context.Comments.Where(c => c.Post.Id == postId).ToListAsync();
        return comments;
    }
}