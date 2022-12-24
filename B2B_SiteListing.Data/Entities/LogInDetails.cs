using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_SiteListing.Data.Entities
{
    public class LogInDetails : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string? Title { get; set; }
        public string? OperatingPersonName { get; set; }
        public string? Designation { get; set; }
        public string? FunctionalDept { get; set; }
        public string? AlternateEmail { get; set; }
        public bool IsAlternateEmailVerified { get; set; }
        public string? ISD_Mobile { get; set; }
        public string? Mobile { get; set; }
        public string? ISD_Whatsapp { get; set; }
        public string? Whatsapp { get; set; }
        public string? RegistrationIPAddress { get; set; }
        public bool ReceiveNotification { get; set; }
        public bool AcceptedTermsAndPolicy { get; set; }

    }
}
