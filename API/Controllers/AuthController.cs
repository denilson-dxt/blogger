using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.RequestModels;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

public class AuthController : BaseAPIController
{
    private readonly IMediator _mediator;
    private readonly IAuthenticateUser _authenticateUser;

    public AuthController(IMediator mediator, IAuthenticateUser authenticateUser)
    {
        _mediator = mediator;
        _authenticateUser = authenticateUser;
    }
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<ActionResult> CreateUser([FromForm]  RegisterModel data)
    {
        var file = data.ProfilePicture;
        var fileStream = new MemoryStream();
        if(file != null)
        {
            await file.CopyToAsync(fileStream);
        }
        var request = new CreateUser.CreateUserCommand
        {
            Email = data.Email,
            UserName = data.UserName,
            Password = data.Password,
            ProfilePicture = fileStream
        };
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateUser(UpdateUser.UpdateUserCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPatch("update-password")]
    public async Task<ActionResult> UpdateUserPassword(UpdateUserPassword.UpdateUserPasswordCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPatch("update-profile-picture")]
    public async Task<ActionResult> UpdateUserProfilePicture([FromForm] IFormFile profilePicture)
    {
        var pictureStream = new MemoryStream();
        await profilePicture.CopyToAsync(pictureStream);
        return Ok(await _mediator.Send(new UpdateUserProfilePicture.UpdateUserProfilePictureCommand
        {
            Id = "d6fb757d-5991-44be-8b4f-68c2ca1ba9a7",
            ProfilePictureStream = pictureStream
        }));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(AuthenticateUser.AuthenticateUserCommand request)
    {
        var user = await _mediator.Send(request);
        if(user == null)
            return BadRequest("User credentials dont match");
        var token =  await _authenticateUser.AuthenticateUser(user) as string;
        return Ok(token);
    }
}