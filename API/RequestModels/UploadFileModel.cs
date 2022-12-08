using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.RequestModels;
public class UploadFileModel
{
    public string? ParentId { get; set; }
    public IFormFile File { get; set; }

}