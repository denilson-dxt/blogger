using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;
public class Folder:BaseEntity
{
    public string? ParentId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<File> Files { get; set; }

}