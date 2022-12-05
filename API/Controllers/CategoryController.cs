using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class CategoryController : BaseAPIController
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
       _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult> Create(CreateCategory.CreateCategoryCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    public async Task<ActionResult> ListAll()
    {
        return Ok(await _mediator.Send(new ListAllCategories.ListAllQuery ()));
    }

    [HttpPut]
    public async Task<ActionResult<CategoryDto>> Update(UpdateCategory.UpdateCategoryCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(DeleteCategory.DeleteCategoryCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}