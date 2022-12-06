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

namespace Application.Posts;
public class GetPostBySlug
{
    public class GetPostBySlugQuery:IRequest<PostDto>
    {
        public string Slug { get; set; }
    }
    public class GetPostBySlugQueryHandler : IRequestHandler<GetPostBySlugQuery, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostBySlugQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<PostDto> Handle(GetPostBySlugQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostBySlug(request.Slug);
            if(post is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Post not found");
            return _mapper.Map<PostDto>(post);
        }
    }
}
