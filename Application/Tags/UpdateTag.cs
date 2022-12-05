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

namespace Application.Tags;

public class UpdateTag
{
    public class UpdateTagCommand:IRequest<TagDto>
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }
    public class UpdateTagCommandHander : IRequestHandler<UpdateTagCommand, TagDto>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public UpdateTagCommandHander(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<TagDto> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetById(request.Id);
            if(tag is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Tag not found");
            
            tag.Description = request.Description;
            await _tagRepository.UpdateAsync(tag);
            await _tagRepository.Complete();
            return _mapper.Map<TagDto>(tag);
        }
    }
}