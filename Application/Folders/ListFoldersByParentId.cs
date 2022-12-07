using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Folders;
public class ListFoldersByParentId
{
    public class ListFoldersByParentIdQuery:IRequest<IEnumerable<FolderDto>>
    {
        public string ParentId { get; set; }
    }
    public class ListFoldersByParentIdQueryHandler : IRequestHandler<ListFoldersByParentIdQuery, IEnumerable<FolderDto>>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public ListFoldersByParentIdQueryHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FolderDto>> Handle(ListFoldersByParentIdQuery request, CancellationToken cancellationToken)
        {
            var parentFolder = await _folderRepository.GetById(request.ParentId);
            if(parentFolder is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Parent folder not found");
            var folders = await _folderRepository.GetByParentId(request.ParentId);
            return _mapper.Map<IEnumerable<FolderDto>>(folders);
        }
    }
}