using Microsoft.AspNetCore.Http;
using NorthwindExample.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Service.Services
{
    public class FileService : IFileService
    {
        public byte[] FileConvertByteToDb(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();                
                return fileBytes;
            }
        }
    }
}
