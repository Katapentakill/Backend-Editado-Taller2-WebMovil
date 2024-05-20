using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using project_dotnet7_api.Src.Helpers;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Services.Implements
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var account = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhoto(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();
            if(photo.Length > 0)
            {
                using var stream = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.FileName,stream),
                    Transformation = new Transformation()
                       .Height(500)
                       .Width(500)
                       .Crop("fill")
                       .Gravity("face"),
                    Folder = "Taller_IDWM"
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhoto(string publicId)
        {
            if(publicId == "Taller_IDWM/hgohlblkjf9ucukczyiy"){
                var mockResult = new DeletionResult
                {
                    Result = "ok",
                    StatusCode = HttpStatusCode.OK
                };
                return mockResult;
            }
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}