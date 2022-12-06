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

namespace Application.Comments;
public class GetCommentsByPostId
{
    public class GetCommentsByPostIdQuery : IRequest<List<CommentDto>>
    {
        public string PostId { get; set; }
    }
    public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, List<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetCommentsByPostIdQueryHandler(ICommentRepository commentRepository,IPostRepository postRepository ,IMapper mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<List<CommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.PostId);
            if(post is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Post not found");
            var comments = await _commentRepository.GetByPostId(post.Id);
            return _mapper.Map<List<CommentDto>>(comments);
        }
    }

}