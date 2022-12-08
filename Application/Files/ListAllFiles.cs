using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Files;
public class ListAllFiles
{
    public class ListAllFilesQuery:IRequest<IEnumerable<FileDto>>
    {

    }
    public class ListAllFilesQueryHandler : IRequestHandler<ListAllFilesQuery, IEnumerable<FileDto>>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public ListAllFilesQueryHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FileDto>> Handle(ListAllFilesQuery request, CancellationToken cancellationToken)
        {
            var files = await _fileRepository.ListAll();
            return _mapper.Map<IEnumerable<FileDto>>(files);
        }
    }
}