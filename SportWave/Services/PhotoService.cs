using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using SportWave.Cloudinary;
using SportWave.Services.Contracts;

namespace TestImgUpload.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0 && (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png"))
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };
                uploadParams.UseFilenameAsDisplayName = true;
                uploadParams.PublicId = uploadParams.File.FileName;
                uploadParams.Overwrite = false;

                uploadResult = await cloudinary.UploadAsync(uploadParams);
                return uploadResult;
            }
            else
            {
                return null;
            }    
        }
    }
}
