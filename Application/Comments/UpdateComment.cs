using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using MediatR;
using Application.Interfaces;
using AutoMapper;

namespace Application.Comments;
public class UpdateComment
{
    public class UpdateCommentCommand : IRequest<CommentDto>
    {
        public string Id { get; set; }
        public string Content { get; set; }

    }
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetById(request.Id);
            if(comment is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Comment not found");
            comment.Content = request.Content;
            await _commentRepository.UpdateAsync(comment);
            await _commentRepository.Complete();
            return _mapper.Map<CommentDto>(comment);
        }
    }
}