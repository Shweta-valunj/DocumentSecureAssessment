namespace DocumentSecureAssessment.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[] Data { get; set; }
        public int Version { get; set; }
        public string UserId { get; set; } // Reference to Identity User
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
