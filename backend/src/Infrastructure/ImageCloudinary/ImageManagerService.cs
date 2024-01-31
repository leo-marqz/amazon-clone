using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.ImageManager;
using Microsoft.Extensions.Options;

namespace Infrastructure.ImageCloudinary
{
    public class ImageManagerService : IImageManagerService
    {
        private readonly CloudinarySettings _cloudinarySettings;
        
        public ImageManagerService(IOptions<CloudinarySettings> cloudinaryOptions)
        {
            _cloudinarySettings = cloudinaryOptions.Value;
        }

        public async Task<ImageResponse> UploadAsync(ImageStream imageStream)
        {
            Account account = new Account(
                cloud: _cloudinarySettings.CloudName, 
                apiKey: _cloudinarySettings.APIKey, 
                apiSecret: _cloudinarySettings.APISecret);

            Cloudinary cloudinary = new Cloudinary(account);

            ImageUploadParams uploadImage = new ImageUploadParams {
                File = new FileDescription(imageStream.Name, imageStream.Image)
            };

            ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadImage);

            if(uploadResult.StatusCode == HttpStatusCode.OK){
                return new ImageResponse {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.Url.ToString()
                };
            }

            throw new Exception("Error, no se pudo guardar la imagen");
        }
    }
}