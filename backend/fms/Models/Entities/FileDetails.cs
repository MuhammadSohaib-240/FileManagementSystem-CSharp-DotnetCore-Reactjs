using System.Text.Json.Serialization;

namespace fms.Models.Entities
{
    public class FileDetails
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public long Length { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public string? FilePath { get; set; }

        // Foreign key for User
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
