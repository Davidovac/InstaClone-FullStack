using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaClone.Application.Interfaces;

namespace InstaClone.Infrastructure.Services
{
    public class LocalImageStorageService : ILocalImageStorageService
    {
        private const long MaxFileSizeInBytes = 5_242_880;
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
        private readonly string _webRootPath;

        public LocalImageStorageService(IWebHostEnvironment environment)
        {
            _webRootPath = environment.WebRootPath;
            if (string.IsNullOrWhiteSpace(_webRootPath))
            {
                _webRootPath = Path.Combine(environment.ContentRootPath, "wwwroot");
            }
        }

        public async Task<string> SavePostImageAsync(IFormFile file, Guid postId)
        {
            ArgumentNullException.ThrowIfNull(file);

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                throw new ValidationException("Only .jpg, .jpeg, .png, and .webp files are allowed.");
            }

            if (file.Length > MaxFileSizeInBytes)
            {
                throw new ValidationException("Image files must be 5 MB or smaller.");
            }

            var restaurantDirectory = Path.Combine(_webRootPath, "images", "posts", postId.ToString());
            Directory.CreateDirectory(restaurantDirectory);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var physicalPath = Path.Combine(restaurantDirectory, fileName);

            await using var stream = File.Create(physicalPath);
            await file.CopyToAsync(stream);

            return $"/images/posts/{postId}/{fileName}";
        }

        public async Task<string> SaveProfileImageAsync(IFormFile file, Guid profileId)
        {
            ArgumentNullException.ThrowIfNull(file);

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                throw new ValidationException("Only .jpg, .jpeg, .png, and .webp files are allowed.");
            }

            if (file.Length > MaxFileSizeInBytes)
            {
                throw new ValidationException("Image files must be 5 MB or smaller.");
            }

            var restaurantDirectory = Path.Combine(_webRootPath, "images", "profile-images", profileId.ToString());
            Directory.CreateDirectory(restaurantDirectory);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var physicalPath = Path.Combine(restaurantDirectory, fileName);

            await using var stream = File.Create(physicalPath);
            await file.CopyToAsync(stream);

            return $"/images/profile-images/{profileId}/{fileName}";
        }
    }
}