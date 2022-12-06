using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain;

public class Category : BaseEntity
{
    public string Description { get; set; }
    public string Slug { get; set; }
    [JsonIgnore]
    public List<Post> Posts { get; set; }
}
