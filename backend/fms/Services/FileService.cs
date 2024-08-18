using fms.Database;
using fms.Models.DTOs;
using fms.Models.Entities;
using fms.Repositories;
using fms.Utilities;

namespace fms.Services
{
    public class FileService : IFileService
    {
        private readonly AppDbContext _context;
        private readonly string _storageRoot = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private readonly string _baseUrl = "https://localhost:7055/api/Files";
        private readonly IUserRepository _userRepository;

        public FileService(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
            FileUtils.CreateDirectories(_storageRoot);
        }

        public async Task<FileDetails> UploadFile(FileUploadDto fileUploadDto, int userId)
        {
            var user = _userRepository.GetById(userId);
            FileUtils.CreateUserDirectories(_storageRoot, user.Username); // Create user-specific directories

            if (user.UsedStorage + fileUploadDto.File.Length > user.UploadLimit)
            {
                throw new Exception("Upload limit exceeded");
            }

            string? folder = FileUtils.GetFolderForFileType(fileUploadDto.File.FileName);
            if (folder == null)
            {
                throw new Exception("Unsupported file type");
            }

            var fileExtension = Path.GetExtension(fileUploadDto.File.FileName);
            var userSpecificFolder = Path.Combine(_storageRoot, user.Username, folder);

            var filePath = Path.Combine(userSpecificFolder, fileUploadDto.CustomFileName + fileExtension);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileUploadDto.File.CopyToAsync(stream);
            }

            var fileDetail = new FileDetails
            {
                Name = fileUploadDto.CustomFileName + fileExtension,
                Length = fileUploadDto.File.Length,
                CreationTime = DateTime.Now,
                LastAccessTime = DateTime.Now,
                LastWriteTime = DateTime.Now,
                FilePath = Path.Combine(user.Username, folder, fileUploadDto.CustomFileName + fileExtension),
                UserId = userId
            };

            _context.FileDetails.Add(fileDetail);
            await _context.SaveChangesAsync();

            user.UsedStorage += fileUploadDto.File.Length;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return fileDetail;
        }

        public FileDetails[] GetAllFiles(int userId)
        {
            var files = _context.FileDetails.Where(f => f.UserId == userId).ToList();
            foreach (var file in files)
            {
                file.FilePath = $"{_baseUrl}/{file.FilePath?.Replace('\\', '/')}";
            }
            return files.ToArray();
        }

        public FileDetails GetFileDetails(int id, int userId)
        {
            var fileDetail = _context.FileDetails.FirstOrDefault(f => f.Id == id && f.UserId == userId);
            if (fileDetail == null)
            {
                throw new Exception("File not found");
            }
            return fileDetail;
        }

        public byte[] GetFile(string folder, string fileName, int userId)
        {
            var user = _userRepository.GetById(userId);
            var userFolder = Path.Combine(_storageRoot, user.Username, folder);
            var fileDetail = _context.FileDetails.FirstOrDefault(f => f.FilePath == Path.Combine(user.Username, folder, fileName) && f.UserId == userId);
            if (fileDetail == null)
            {
                throw new Exception("File not found");
            }

            var filePath = Path.Combine(userFolder, fileName);
            if (!File.Exists(filePath))
            {
                throw new Exception("File not found");
            }
            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetails> RenameFile(int id, string newFileName, int userId)
        {
            var fileDetail = _context.FileDetails.FirstOrDefault(f => f.Id == id && f.UserId == userId);
            if (fileDetail == null)
            {
                throw new Exception("File not found");
            }

            var user = _userRepository.GetById(userId);
            var userFolder = Path.Combine(_storageRoot, user.Username);
            string folder = FileUtils.GetFolderForFileType(fileDetail.Name);
            if (folder == null)
            {
                throw new Exception("Unsupported file type");
            }

            var fileExtension = Path.GetExtension(fileDetail.Name);
            var newFilePath = Path.Combine(userFolder, folder, newFileName + fileExtension);

            if (File.Exists(newFilePath))
            {
                throw new Exception("A file with the new name already exists");
            }

            var oldFilePath = Path.Combine(_storageRoot, fileDetail.FilePath);
            File.Move(oldFilePath, newFilePath);

            fileDetail.Name = newFileName + fileExtension;
            fileDetail.FilePath = Path.Combine(user.Username, folder, newFileName + fileExtension);
            await _context.SaveChangesAsync();

            return fileDetail;
        }

        public async Task DeleteFile(int id, int userId)
        {
            var fileDetail = _context.FileDetails.FirstOrDefault(f => f.Id == id && f.UserId == userId);
            if (fileDetail == null)
            {
                throw new Exception("File not found");
            }

            var filePath = Path.Combine(_storageRoot, fileDetail.FilePath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.UsedStorage -= fileDetail.Length;
                _context.Users.Update(user);
            }

            _context.FileDetails.Remove(fileDetail);
            await _context.SaveChangesAsync();
        }
    }
}
