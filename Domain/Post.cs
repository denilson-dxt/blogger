using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;
public class Post:BaseEntity
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    public User User { get; set; }
    public DateTime PublishedAt{get;set;} = DateTime.Now;
    public DateTime EditedAt{get;set;} = DateTime.Now;
    public List<Category> Categories { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Comment> Comments { get; set; }
}
