using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;

namespace Application.Files;
public class DeleteFile
{
    public class DeleteFileCommand:IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, bool>
    {
        private readonly IFileRepository _fileRepository;

        public DeleteFileCommandHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetById(request.Id);
            if(file is null) 
                throw new ApiException((int)HttpStatusCode.NotFound, "File not found");

            await _fileRepository.DeleteAsync(file);
            await _fileRepository.Complete();
            return true;
        }
    }
}