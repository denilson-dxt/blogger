using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Category
{
    public string Id { get; set; }
    public string Description {get;set;}
    public string Slug{get;set;}
    public List<Post> Posts { get; set; }
}
