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

namespace Application.Posts;
public class ListPostsByCategory
{
    public class ListPostsByCategoryQuery:IRequest<IEnumerable<PostDto>>
    {
        public string CategorySlug { get; set; }
    }
    public class ListPostsByCategoryQueryHandler : IRequestHandler<ListPostsByCategoryQuery, IEnumerable<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ListPostsByCategoryQueryHandler(IMapper mapper, IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }
        public async Task<IEnumerable<PostDto>> Handle(ListPostsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetBySlug(request.CategorySlug);
            if(category is null) throw new ApiException((int) HttpStatusCode.NotFound, "Category not found");
            var posts = await _postRepository.FilterMany(p => p.Categories.Contains(category));
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }
}