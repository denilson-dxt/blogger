using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain;
public class File:BaseEntity
{
    public string Name { get; set; }
    public string Path { get; set; }
    [JsonIgnore]
    public Folder ParentFolder{get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}