using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CloudinaryService
{
    public interface ICloudinaryService
    {
        public Task<ImageUploadResult> AddPhoto(IFormFile file);

        public Task<DeletionResult> DeletePhoto(string publicId);
    }
}
