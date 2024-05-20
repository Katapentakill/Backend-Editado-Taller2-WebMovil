using CloudinaryDotNet.Actions;

namespace project_dotnet7_api.Src.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhoto(IFormFile photo);

        Task<DeletionResult> DeletePhoto(string publicId);        
    }
}