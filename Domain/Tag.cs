using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain;

public class Tag:BaseEntity
{
    public string Description { get; set; }
    [JsonIgnore]
    public List<Post> Posts { get; set; }
}
