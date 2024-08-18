using System.Text.Json.Serialization;

namespace fms.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public long UploadLimit { get; set; }
        public long UsedStorage { get; set; }

        [JsonIgnore]
        public ICollection<FileDetails>? Files { get; set; }
    }
}
