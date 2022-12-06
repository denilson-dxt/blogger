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
using Domain;

namespace Application.Posts;
public class UpdatePost
{
    public class UpdatePostCommand : IRequest<PostDto>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public List<string> CommentsId { get; set; }
        public List<string> TagsId { get; set; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public UpdatePostCommandHandler(IPostRepository postRepository, IMapper mapper, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }
        public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.Id);
            if (post is null)
                throw new ApiException((int)HttpStatusCode.NotFound, "Post not found");

            var categories = await _categoryRepository.FilterMany(c => request.CommentsId.Contains(c.Id));
            var tags = await _tagRepository.FilterMany(t => request.TagsId.Contains(t.Id));


            post.Categories = categories.ToList<Category>();
            post.Tags = tags.ToList<Tag>();
            post.Title = request.Title;
            post.Image = request.Image;
            post.Slug = request.Slug;
            post.Content = request.Content;
            post.EditedAt = DateTime.UtcNow;

            await _postRepository.UpdateAsync(post);
            await _postRepository.Complete();

            return _mapper.Map<PostDto>(post);

        }
    }
}