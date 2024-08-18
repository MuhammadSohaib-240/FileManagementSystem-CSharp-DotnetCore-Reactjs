using fms.Models.DTOs;
using fms.Models.Entities;

namespace fms.Services
{
    public interface IFileService
    {
        Task<FileDetails> UploadFile(FileUploadDto fileUploadDto, int userId);
        FileDetails[] GetAllFiles(int userId);
        FileDetails GetFileDetails(int id, int userId);
        byte[] GetFile(string folder, string fileName, int userId);
        Task<FileDetails> RenameFile(int id, string newFileName, int userId);
        Task DeleteFile(int id, int userId);
    }
}
