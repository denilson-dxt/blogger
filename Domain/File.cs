using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;
public class File:BaseEntity
{
    public string Name { get; set; }
    public string Path { get; set; }
    public Folder ParentFolder{get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}