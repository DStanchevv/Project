using CloudinaryDotNet.Actions;

namespace SportWave.Services.Contracts
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    }
}
