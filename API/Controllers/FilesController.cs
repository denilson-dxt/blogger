using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.RequestModels;
using Application.Dtos;
using Application.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class FilesController:BaseAPIController
{
    private readonly IMediator _mediator;

    public FilesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult<FileDto>> UploadFile([FromForm]UploadFileModel request)
    {
        var file = await UploadedFileHelper.ConvertFileToMemoryStream(request.File);
        var fileName = UploadedFileHelper.GetFileNameFromFormFile(request.File);
        return Ok(await _mediator.Send(new CreateFile.CreateFileCommand
        {
            Name = fileName,
            File = file,
            ParentId = request.ParentId
        }));
    }

    [HttpPut]
    public async Task<ActionResult<FileDto>> UpdateFile(UpdateFile.UpdateFileCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteFile(DeleteFile.DeleteFileCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("parent/{parentId}")]
    public async Task<ActionResult<FileDto>> ListByParentId(string parentId)
    {
        return Ok(await _mediator.Send(new ListFilesByParentId.ListFilesByParentIdQuery()
        {
            ParentId = parentId
        }));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FileDto>>> ListAllFiles()
    {
        return Ok(await _mediator.Send(new ListAllFiles.ListAllFilesQuery()));
    }
}

