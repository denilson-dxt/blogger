using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces;
public interface IFileUploader
{
    public Task<string> UploadFromStream(Stream stream);
    public Task<bool> DeleteUploadedFile(string path);
}