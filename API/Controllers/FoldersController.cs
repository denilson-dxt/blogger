using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Folders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class FoldersController : BaseAPIController
{
    private readonly IMediator _mediator;

    public FoldersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<FolderDto>> Create(CreateFolder.CreateFolderCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    public async Task<ActionResult<FolderDto>> Update(UpdateFolder.UpdateFolderCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FolderDto>>> ListAll()
    {
        return Ok(await _mediator.Send(new ListAllFolders.ListAllFolderQuery()));
    }

    [HttpGet("parent/{parentId}")]
    public async Task<ActionResult<IEnumerable<FolderDto>>> ListByParentId(string parentId)
    {
        return Ok(await _mediator.Send(new ListFoldersByParentId.ListFoldersByParentIdQuery()
        {
            ParentId = parentId
        }));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(DeleteFolder.DeleteFolderCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}