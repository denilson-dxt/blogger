using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;

namespace Application.Folders;
public class DeleteFolder
{
    public class DeleteFolderCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, bool>
    {
        private readonly IFolderRepository _folderRepository;
        public DeleteFolderCommandHandler(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;

        }
        public async Task<bool> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            var folder = await _folderRepository.GetById(request.Id);
            if (folder is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Folder not found");

            await _folderRepository.DeleteAsync(folder);
            await _folderRepository.Complete();
            return true;
        }
    }
}