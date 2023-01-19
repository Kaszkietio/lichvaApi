namespace API.Entities
{
    public class AppSettings
    {
        public static AppSettings Instance { get; set; } = new AppSettings();
        public string JwtSecret { get; set; } = string.Empty;
        public string GoogleClientId { get; set; } = string.Empty;
        public string GoogleClientSecret { get; set; } = string.Empty;
        public string JwtEmailEncryption { get; set; } = string.Empty;
        public string HashKey { get; set; } = string.Empty;
        public string BlobConnectionString { get; set; } = string.Empty;
        public string BlobContainerName { get; set; } = string.Empty;
        public string BlobUrl { get; set; } = string.Empty;
        public string ContractKey { get; set; } = string.Empty;
    }
}
