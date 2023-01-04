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
public class ListPostsByTag
{
    public class ListPostsByTagQuery:IRequest<IEnumerable<PostDto>>
    {
        public string TagDescription { get; set; }
    }
    public class ListPostsByTagQueryHandler : IRequestHandler<ListPostsByTagQuery, IEnumerable<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;

        public ListPostsByTagQueryHandler(IMapper mapper, IPostRepository postRepository, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IEnumerable<PostDto>> Handle(ListPostsByTagQuery request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.FilterOne(t => t.Description == request.TagDescription);
            if(tag is null) throw new ApiException((int)HttpStatusCode.NotFound, "Tag not found");

            var posts = await _postRepository.FilterMany(p => p.Tags.Contains(tag));
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }
}