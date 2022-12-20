using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;

namespace Application.Users;
public class UpdateUserProfilePicture
{
    public class UpdateUserProfilePictureCommand : IRequest<string>
    {
        public MemoryStream ProfilePictureStream;
    }
    public class UpdateUserProfilePictureCommandHandler : IRequestHandler<UpdateUserProfilePictureCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IUserService _userService;

        public UpdateUserProfilePictureCommandHandler(IUserRepository userRepository, IFileUploader fileUploader, IUserService userService)
        {
            _userRepository = userRepository;
            _fileUploader = fileUploader;
            _userService = userService;
        }
        public async Task<string> Handle(UpdateUserProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetActualUser();
            if (user is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "User not found");

            string fileName = Guid.NewGuid().ToString() + ".png";
            user.ProfilePicture = await _fileUploader.UploadFromStream(request.ProfilePictureStream, fileName);
            await _userRepository.UpdateUser(user);
            return user.ProfilePicture;
        }
    }
}