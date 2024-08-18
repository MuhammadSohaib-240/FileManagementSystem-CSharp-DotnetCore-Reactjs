namespace fms.Utilities
{
    public static class FileUtils
    {
        public static void CreateDirectories(string storageRoot)
        {
            // Ensure the root storage directory exists
            if (!Directory.Exists(storageRoot))
            {
                Directory.CreateDirectory(storageRoot);
            }
        }

        public static void CreateUserDirectories(string storageRoot, string username)
        {
            // Create user-specific directories for each file type
            var directories = new[] { "images", "pdfs", "docs" };
            foreach (var dir in directories)
            {
                var userSpecificPath = Path.Combine(storageRoot, username, dir);
                if (!Directory.Exists(userSpecificPath))
                {
                    Directory.CreateDirectory(userSpecificPath);
                }
            }
        }

        public static string? GetFolderForFileType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" or ".png" or ".gif" => "images",
                ".pdf" => "pdfs",
                ".doc" or ".docx" or ".txt" => "docs",
                _ => null
            };
        }

        public static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".doc" or ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };
        }
    }
}
