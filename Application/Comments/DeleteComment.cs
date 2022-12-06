using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Comments;

public class DeleteComment
{
    public class DeleteCommentCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetById(request.Id);
            if (comment is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Comment not found");
        
            await _commentRepository.DeleteAsync(comment);
            await _commentRepository.Complete();
            return true;
        }
    }
}