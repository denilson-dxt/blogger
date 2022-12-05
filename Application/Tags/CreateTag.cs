using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Tags;
public class CreateTag
{
    public class CreateTagCommand:IRequest<TagDto>
    {
        public string Description { get; set; }
    }
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagDto>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<TagDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = new Tag
            {
                Id=Guid.NewGuid().ToString(),
                Description = request.Description
            };
            await _tagRepository.CreateAsync(tag);
            await _tagRepository.Complete();
            return _mapper.Map<TagDto>(tag);
        }
    }
}
