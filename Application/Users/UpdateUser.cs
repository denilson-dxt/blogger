using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Users;

public class UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetActualUser();
            if (user is null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "User not found");
            }
            user.UserName = request.UserName;
            try
            {
                await _userRepository.UpdateUser(user);

            }
            catch(ApiException e)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, e.Errors);
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}