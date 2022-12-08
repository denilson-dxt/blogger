using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Folders;
public class ListAllFolders
{
    public class ListAllFolderQuery : IRequest<IEnumerable<FolderDto>>
    {

    }
    public class ListAllFolderQueryHandler : IRequestHandler<ListAllFolderQuery, IEnumerable<FolderDto>>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public ListAllFolderQueryHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FolderDto>> Handle(ListAllFolderQuery request, CancellationToken cancellationToken)
        {
            var folders = await _folderRepository.ListAll();
            return _mapper.Map<IEnumerable<FolderDto>>(folders);
            
        }
    }
}