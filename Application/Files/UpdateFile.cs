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
public class UpdateFile
{
    public class UpdateFileCommand : IRequest<FileDto>
    {
        public string Id { get; set; }
        public string? ParentId { get; set; }
        public string Name { get; set; }
    }
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, FileDto>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IFolderRepository _folderRepository;

        public UpdateFileCommandHandler(IFileRepository fileRepository, IMapper mapper, IFolderRepository folderRepository)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _folderRepository = folderRepository;
        }
        public async Task<FileDto> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetById(request.Id);
            if (file is null) throw new ApiException((int)HttpStatusCode.NotFound, "File not found");

            Domain.Folder folder = null;

            if (request.ParentId is not null)
            {
                folder = await _folderRepository.GetById(request.ParentId);
                if (folder is null) throw new ApiException((int)HttpStatusCode.NotFound, "Folder not found");

            }
            file.Name = request.Name;
            file.ParentFolder = folder;

            await _fileRepository.UpdateAsync(file);
            await _fileRepository.Complete();

            return _mapper.Map<FileDto>(file);
        }
    }
}