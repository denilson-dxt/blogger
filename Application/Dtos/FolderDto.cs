using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos;
public class FolderDto
{
    public string Id { get; set; }
    public string ParentId { get; set; }
    public string Name { get; set; }
     public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<FileDto> Files { get; set; }
}