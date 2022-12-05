using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Categories;
public class ListAllCategories
{
    public class ListAllQuery:IRequest<IEnumerable<CategoryDto>>
    {

    }
    public class ListAllQueryHandler : IRequestHandler<ListAllQuery, IEnumerable<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public ListAllQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(ListAllQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.ListAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}
