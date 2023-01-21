using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Core.Services
{ 
    public interface IFileService
    {
        byte[] FileConvertByteToDb(IFormFile file);
    }
}
