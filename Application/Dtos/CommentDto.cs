using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain;

namespace Application.Dtos;
public class CommentDto
{
    public string Id { get; set; }
    public string Content { get; set; }
    public UserDto Owner { get; set; }
    
    [JsonIgnore]
    public PostDto Post { get; set; }
    public DateTime PublishedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}