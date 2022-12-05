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

namespace Application.Categories;

public class CreateCategory
{
    public class CreateCategoryCommand:IRequest<CategoryDto>
    {
        public string Description { get; set; }
        public string Slug { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var dbCategory = await _categoryRepository.GetBySlug(request.Slug);
            if(dbCategory is not null)
                throw new ApiException((int)HttpStatusCode.Conflict, "This slug has been already taken");
            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Description = request.Description,
                Slug = request.Slug
            };
            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.Complete();
            return _mapper.Map<CategoryDto>(category);
        }
    }

}
