using DocStream.FileEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Core.Interfaces
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType);

        public Task PostMultiFileAsync(List<FileUploadModel> fileData);

        public Task DownloadFileById(int fileName);
    }
}
