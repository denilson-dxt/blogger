using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Dtos;
public class FileDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public Folder ParentFolder{get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}