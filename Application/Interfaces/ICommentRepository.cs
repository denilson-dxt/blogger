using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces;
public interface ICommentRepository : IGenericRepository<Comment>
{
    public Task<IEnumerable<Comment>> GetByPostId(string postId);   
}