using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Comments;
public class ListAllComments
{
    public class ListAllCommentsQuery:IRequest<List<CommentDto>>
    {

    }
    public class ListAllCommentsQueryHandler : IRequestHandler<ListAllCommentsQuery, List<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public ListAllCommentsQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<List<CommentDto>> Handle(ListAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.ListAll();
            return  _mapper.Map<List<CommentDto>>(comments);
        }
    }
}