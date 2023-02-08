using System.ComponentModel.DataAnnotations;

namespace FlowCSFinal.Models
{
    public class Users
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Emri { get; set; }

        [Required]
        public string Mbiemri { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
