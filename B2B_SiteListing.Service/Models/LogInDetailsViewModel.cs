using System.ComponentModel.DataAnnotations;

namespace B2B_SiteListing.Service.Models
{
    public class LogInDetailsViewModel
    {
        public string? Title { get; set; }
        public string? OperatingPersonName { get; set; }
        public string? Designation { get; set; }
        public string? FunctionalDept { get; set; }
        public string? AlternateEmail { get; set; }
        public bool IsAlternateEmailVerified { get; set; }
        [Required]
        public string? ISD_Mobile { get; set; }
        [Required]
        public string? Mobile { get; set; }
        public string? ISD_Whatsapp { get; set; }
        public string? Whatsapp { get; set; }
        public string? RegistrationIPAddress { get; set; }
        public bool ReceiveNotification { get; set; }
        public bool AcceptedTermsAndPolicy { get; set; }
    }
}
