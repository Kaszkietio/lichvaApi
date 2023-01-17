namespace API.Entities
{
    public class AppSettings
    {
        public static AppSettings appSettings { get; set; } = new AppSettings();
        public string JwtSecret { get; set; } = string.Empty;
        public string GoogleClientId { get; set; } = string.Empty;
        public string GoogleClientSecret { get; set; } = string.Empty;
        public string JwtEmailEncryption { get; set; } = string.Empty;
    }
}
