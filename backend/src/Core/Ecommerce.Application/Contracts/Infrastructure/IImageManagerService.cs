using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Models.ImageManager;

namespace Ecommerce.Application.Contracts.Infrastructure
{
    public interface IImageManagerService
    {
        Task<ImageResponse> UploadAsync(ImageStream imageStream);
    }
}