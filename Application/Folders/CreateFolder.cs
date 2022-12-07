using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Folders;
public class CreateFolder
{
    public class CreateFolderCommand:IRequest<FolderDto>
    {
        public string? ParentId { get; set; }
        public string Name { get; set; }
    }
    public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, FolderDto>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public CreateFolderCommandHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<FolderDto> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            var folder = new Domain.Folder
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                ParentId = request.ParentId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _folderRepository.CreateAsync(folder);
            await _folderRepository.Complete();
            return _mapper.Map<FolderDto>(folder);
        }
    }
}