using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Users;
public class AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public AuthenticateUserCommandHandler(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }
        public async Task<User> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if(user is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "User not found");
            return await _userService.AuthenticateUser(user, request.Password);
        }
    }
}