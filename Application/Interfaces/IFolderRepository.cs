using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces;
public interface IFolderRepository:IGenericRepository<Domain.Folder>
{
    public Task<IEnumerable<Domain.Folder>> GetByParentId(string parentId);
}