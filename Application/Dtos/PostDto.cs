using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Dtos;
public class PostDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    public UserDto User { get; set; }
    public DateTime PublishedAt{get;set;} = DateTime.Now;
    public DateTime EditedAt{get;set;} = DateTime.Now;
    public List<CategoryDto> Categories { get; set; }
    public List<TagDto> Tags { get; set; }
    public List<Comment> Comments { get; set; }
}