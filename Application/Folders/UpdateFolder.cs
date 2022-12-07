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
public class UpdateFolder
{
    public class UpdateFolderCommand : IRequest<FolderDto>
    {
        public string Id { get; set; }
        public string? ParentId { get; set; }
        public string Name { get; set; }
    }
    public class UpdateFolderCommandHandler : IRequestHandler<UpdateFolderCommand, FolderDto>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public UpdateFolderCommandHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<FolderDto> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
        {
            var folder = await _folderRepository.GetById(request.Id);
            if (folder is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Folder not found");

            folder.Name = request.Name;
            folder.ParentId = request.ParentId;
            await _folderRepository.UpdateAsync(folder);
            await _folderRepository.Complete();
            return _mapper.Map<FolderDto>(folder);
        }
    }
}