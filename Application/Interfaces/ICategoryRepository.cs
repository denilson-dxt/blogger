using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces;
public interface ICategoryRepository:IGenericRepository<Category>
{
    public Task<Category> GetBySlug(string slug);
}
