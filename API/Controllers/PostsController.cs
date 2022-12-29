using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class PostsController:BaseAPIController
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpPost]
    public async Task<ActionResult> Create(CreatePost.CreatePostCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<PostDto>>> ListAll()
    {
        return Ok(await _mediator.Send(new ListAllPosts.ListAllPostsQuery()));
    }

    [HttpGet("category/{categorySlug}")]
    [AllowAnonymous]
    public async Task<IEnumerable<PostDto>> ListByCategory(string categorySlug)
    {
        ListPostsByCategory.ListPostsByCategoryQuery request = new ListPostsByCategory.ListPostsByCategoryQuery()
        {
            CategorySlug = categorySlug
        };
        return await _mediator.Send(request);
    }


    [HttpGet("{slug}")]
    [AllowAnonymous]
    public async Task<ActionResult<PostDto>> GetBySlug(string slug)
    {
        return (await _mediator.Send(new GetPostBySlug.GetPostBySlugQuery()
        {
            Slug = slug
        }));
    }

    [HttpPut]
    public async Task<ActionResult<PostDto>> UpdatePost(UpdatePost.UpdatePostCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeletePost(DeletePost.DeletePostCommand request)
    {
        return Ok(await _mediator.Send(request));
    }


}
