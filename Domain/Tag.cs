using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Tag:BaseEntity
{
    public string Description { get; set; }
    public List<Post> Posts { get; set; }
}
