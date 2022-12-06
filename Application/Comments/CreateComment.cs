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

namespace Application.Comments;
public class CreateComment
{
    public class CreateCommentCommand : IRequest<CommentDto>
    {
        public string Content { get; set; }
        public string PostId{get;set;}
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IUserService _userService;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository, IUserService userService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _userService = userService;
        }
        public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.PostId);
            if(post is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Post not found");
            var user = await _userService.GetActualUser();
            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content,
                Post = post,
                Owner = user,
                PublishedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _commentRepository.CreateAsync(comment);
            await _commentRepository.Complete();
            return _mapper.Map<CommentDto>(comment);
        }
    }
}