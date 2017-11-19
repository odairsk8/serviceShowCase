using GC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using GC.Core.Entities;

namespace GC.Core.Services
{
    public class FileSystemPhotoStorage : IPhotoStorage
    {
        public void removeFile(string fileName, string uploadsFolderPath)
        {
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public async Task<string> StorePhoto(string uploadsFolderPath, IFormFile file)
        {
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }
    }
}
