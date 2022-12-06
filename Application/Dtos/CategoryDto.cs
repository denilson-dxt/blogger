using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain;

namespace Application.Dtos;

public class CategoryDto
{
    public string Id { get; set; }
    public string Description {get;set;}
    public string Slug{get;set;}
    [JsonIgnore]
    public List<Post> Posts { get; set; }
}
