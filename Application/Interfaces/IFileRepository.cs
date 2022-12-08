using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces;
public interface IFileRepository:IGenericRepository<Domain.File>
{
    public Task<IEnumerable<Domain.File>> ListByParentId(string parentId);
}