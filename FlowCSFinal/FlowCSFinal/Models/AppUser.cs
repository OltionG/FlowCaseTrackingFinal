using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlowCSFinal.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Emri { get; set; }
        [Required]
        public string Mbiemri { get; set; }
    }
}
