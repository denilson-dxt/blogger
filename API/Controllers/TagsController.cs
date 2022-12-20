using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Tags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class TagsController:BaseAPIController
{
    private readonly IMediator _mediator;

    public TagsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult<TagDto>> Create(CreateTag.CreateTagCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> ListAll()
    {
        return Ok(await _mediator.Send(new ListAllTags.ListAllTagsQuery()));
    }

    [HttpPut]
    public async Task<ActionResult<TagDto>> Update(UpdateTag.UpdateTagCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteTag(DeleteTag.DeleteTagCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}
