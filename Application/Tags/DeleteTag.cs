using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Tags;
public class DeleteTag
{
    public class DeleteTagCommand:IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, bool>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public DeleteTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetById(request.Id);
            if(tag is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Tag not found");
            
            await _tagRepository.DeleteAsync(tag);
            await _tagRepository.Complete();
            return true;
            
        }
    }
}