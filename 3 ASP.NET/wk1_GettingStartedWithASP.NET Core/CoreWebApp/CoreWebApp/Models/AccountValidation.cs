using System.ComponentModel.DataAnnotations;

namespace CoreWebApp.Models
{
    public class AccountValidationModel
    {
        // Make this field required
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string? User { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string? Pass { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string? Email {  get; set; }

        [Url]
        public string? Website { get; set; }

    }
}
