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

namespace Application.Categories;
public class UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<CategoryDto>
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id);
            if (category is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Category not found");

            category.Description = request.Description;
            category.Slug = request.Slug;
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.Complete();
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
