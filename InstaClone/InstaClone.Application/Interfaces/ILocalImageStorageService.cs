using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Interfaces
{
    public interface ILocalImageStorageService
    {
        Task<string> SavePostImageAsync(IFormFile file, Guid postId);
        Task<string> SaveProfileImageAsync(IFormFile file, Guid profileId);
    }
}
