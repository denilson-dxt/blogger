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
public class UpdateUserPassword
{
    public class UpdateUserPasswordCommand : IRequest<UserDto>
    {
        public string Password { get; set; }
    }
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UpdateUserPasswordCommandHandler(IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<UserDto> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetActualUser();
            if(user == null)
                throw new ApiException((int)HttpStatusCode.NotFound, "User not found");
            await _userRepository.UpdateUserPassword(user, request.Password);
            return _mapper.Map<UserDto>(user);
        }
    }
}
