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

namespace Application.Files;
public class ListFilesByParentId
{
    public class ListFilesByParentIdQuery:IRequest<IEnumerable<FileDto>>
    {
        public string ParentId { get; set; }
    }
    public class ListFilesByParentIdQueryHandler : IRequestHandler<ListFilesByParentIdQuery, IEnumerable<FileDto>>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IFolderRepository _folderRepository;

        public ListFilesByParentIdQueryHandler(IFileRepository fileRepository, IMapper mapper, IFolderRepository folderRepository)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _folderRepository = folderRepository;
        }
        public async Task<IEnumerable<FileDto>> Handle(ListFilesByParentIdQuery request, CancellationToken cancellationToken)
        {
            var folder = await _folderRepository.GetById(request.ParentId);
            if(folder is null) throw new ApiException((int)HttpStatusCode.NotFound, "Folder not found");
            
            var files = await _fileRepository.ListByParentId(request.ParentId);
            return _mapper.Map<IEnumerable<FileDto>>(files);
        }
    }
}