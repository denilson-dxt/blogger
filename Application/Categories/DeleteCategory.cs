using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Categories;

public class DeleteCategory
{
    public class DeleteCategoryCommand:IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id);
            if(category is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Category not found");
            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.Complete();
            return true;
            
        }
    }
}
