using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Tags;
public class ListAllTags
{
    public class ListAllTagsQuery:IRequest<IEnumerable<TagDto>>
    {

    }
    public class ListAllTagsQueryHanlder : IRequestHandler<ListAllTagsQuery, IEnumerable<TagDto>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public ListAllTagsQueryHanlder(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TagDto>> Handle(ListAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _tagRepository.ListAll();
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }
    }
}