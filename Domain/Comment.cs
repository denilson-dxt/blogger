using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public User Owner { get; set; }
    public Post Post { get; set; }
    public DateTime PublishedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
}