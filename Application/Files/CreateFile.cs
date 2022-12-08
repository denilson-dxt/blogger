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
public class CreateFile
{
    public class CreateFileCommand : IRequest<FileDto>
    {
        public string Name { get; set; }
        public string? ParentId { get; set; }
        public MemoryStream File { get; set; }
    }
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, FileDto>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IFolderRepository _folderRepository;
        private readonly IFileUploader _fileUploader;

        public CreateFileCommandHandler(IFileRepository fileRepository, IMapper mapper, IFolderRepository folderRepository, IFileUploader fileUploader)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _folderRepository = folderRepository;
            _fileUploader = fileUploader;
        }
        public async Task<FileDto> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var parentfolder = await _folderRepository.GetById(request.ParentId);
            if (request.ParentId is not null)
            {

                if (parentfolder is null)
                    throw new ApiException((int)HttpStatusCode.NotFound, "Parent folder not found");
            }
            var path = await _fileUploader.UploadFromStream(request.File);
            var file = new Domain.File
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                ParentFolder = parentfolder,
                Path = path,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _fileRepository.CreateAsync(file);
            await _fileRepository.Complete();
            return _mapper.Map<FileDto>(file);
        }
    }
}