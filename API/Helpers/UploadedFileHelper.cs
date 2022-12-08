using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers;
public class UploadedFileHelper
{
    public static async Task<MemoryStream> ConvertFileToMemoryStream(IFormFile fileForm)
    {
        var file = new MemoryStream();
        await fileForm.CopyToAsync(file);
        return file;
    }
    public static string GetFileNameFromFormFile(IFormFile formFile)
    {
        return formFile.FileName;
    }
}