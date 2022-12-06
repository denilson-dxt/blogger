using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Comments;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class CommentController :BaseAPIController
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create(CreateComment.CreateCommentCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> ListAll()
    {
        return Ok(await _mediator.Send(new ListAllComments.ListAllCommentsQuery()));
    }

    [HttpPut]
    public async Task<ActionResult<CommentDto>> Update(UpdateComment.UpdateCommentCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(DeleteComment.DeleteCommentCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("post/{postId}")]
    public async Task<ActionResult<List<CommentDto>>> GetByPostId(string postId)
    {
        System.Console.WriteLine(postId);
        return Ok(await _mediator.Send(new GetCommentsByPostId.GetCommentsByPostIdQuery()
        {
            PostId = postId
        }));
    }
}