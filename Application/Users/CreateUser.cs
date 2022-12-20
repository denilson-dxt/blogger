using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Users;
public class CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public MemoryStream? ProfilePicture;
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploader _fileUploader;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IFileUploader fileUploader)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _fileUploader = fileUploader;
        }
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            string profilePicturePath = null;
            if (request.ProfilePicture != null)
            {
                string fileName = Guid.NewGuid().ToString() + ".png";
                profilePicturePath = await _fileUploader.UploadFromStream(request.ProfilePicture, fileName);
            }
            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                ProfilePicture = profilePicturePath
            };

            var result = await _userRepository.CreateUserAsync(user, request.Password);
            if (!result.Succeeded)
            {
                await _fileUploader.DeleteUploadedFile(profilePicturePath);
                throw new ApiException((int)HttpStatusCode.BadRequest, result.Errors);
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}