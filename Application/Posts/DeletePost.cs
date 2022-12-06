using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Posts;
public class DeletePost
{   
    public class DeletePostCommand:IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.Id);
            if(post is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Post not found");
            
            await _postRepository.DeleteAsync(post);
            await _postRepository.Complete();

            return true;
        }
    }
}