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

namespace Application.Posts;
public class CreatePost
{
    public class CreatePostCommand : IRequest<PostDto>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public List<string> CommentsId { get; set; }
        public List<string> TagsId { get; set; }
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserService _userService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IPostRepository postRepository,IUserService userService, ICategoryRepository categoryRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userService = userService;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var dbPost = await _postRepository.GetPostBySlug(request.Slug);
            if(dbPost is not null)
                throw new ApiException((int)HttpStatusCode.Conflict, "This slug already exists, slug must be unique");
            
            var user = await _userService.GetActualUser();
            var categories = await _categoryRepository.FilterMany(c => request.CommentsId.Contains(c.Id));
            var tags = await _tagRepository.FilterMany(t => request.TagsId.Contains(t.Id));
            var post = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Slug = request.Slug,
                Content = request.Content,
                PublishedAt = DateTime.UtcNow,
                EditedAt = DateTime.UtcNow,
                User = user,
                Categories = categories.ToList<Category>(),
                Tags = tags.ToList<Tag>()
            };
            await _postRepository.CreateAsync(post);
            await _postRepository.Complete();
            return _mapper.Map<PostDto>(post);
        }
    }
}