using Microsoft.AspNetCore.Identity;
using System;


namespace B2B_SiteListing.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
