using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Infrastructure.Services;
public class LocalFileUploader : IFileUploader
{

    public async Task<string> UploadFromStream(Stream stream, string? filename)
    {
        string filePath = Path.Join("Uploads", filename);
        if (!Directory.Exists("Uploads"))
            Directory.CreateDirectory("Uploads");


        using (var file = new FileStream(filePath, FileMode.Create))
        {
            stream.Position = 0;
            await stream.CopyToAsync(file);
        }
        return filePath;
    }
    public async Task<bool> DeleteUploadedFile(string path)
    {
        var result = false;
        if (File.Exists(path))
        {
            File.Delete(path);
            result = true;
        }
        return result;
    }
}