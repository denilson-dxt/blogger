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
public class ListAllPosts
{
    public class ListAllPostsQuery:IRequest<IEnumerable<PostDto>>
    {

    }
    public class ListAllPostsQueryHandler : IRequestHandler<ListAllPostsQuery, IEnumerable<PostDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public ListAllPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostDto>> Handle(ListAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.ListAll();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }
}