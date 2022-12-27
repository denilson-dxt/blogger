using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Users;
public class GetLoggedUser
{
    public class GetLoggedUserQuery:IRequest<UserDto>
    {

    }
    public class GetLoggedUserQueryHandler : IRequestHandler<GetLoggedUserQuery, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetLoggedUserQueryHandler(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
        _userService = userService;
            
        }
        public async Task<UserDto> Handle(GetLoggedUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetActualUser();
            return _mapper.Map<UserDto>(user);
        }
    }
}