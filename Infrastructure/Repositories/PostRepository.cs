using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Post> GetById(string id)
        {
            return await _context.Posts.Include(p => p.User).Include(p => p.Categories).Include(p => p.Comments).Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);

        }
        public async Task<Post> GetPostBySlug(string slug)
        {
            var post = await _context.Posts.Include(p => p.User).Include(p => p.Categories).Include(p=>p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
            return post;
        }

        public async Task<IEnumerable<Post>> ListAll()
        {
            return await _context.Posts.Include(p => p.User).Include(p => p.Categories).Include(p => p.Tags).ToListAsync();
        }
    }
}