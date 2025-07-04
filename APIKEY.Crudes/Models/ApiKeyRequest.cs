namespace APIKEY.Crudes.Models
{
    public class ApiKeyRequest
    {
        public string? LicenseKey { get; set; }
        public string? CreatedBy { get; set; }
        public string? Remarks { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
