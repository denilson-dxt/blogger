using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces;
public interface IPostRepository:IGenericRepository<Post>
{
    public Task<Post> GetPostBySlug(string slug);
}