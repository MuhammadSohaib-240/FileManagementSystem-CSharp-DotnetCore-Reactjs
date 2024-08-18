namespace fms.Models.DTOs
{
    public class FileUploadDto
    {
        public string? CustomFileName { get; set; }
        public IFormFile? File { get; set; }
    }
}
