using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialConnect.DTOs.User
{
    public class UpdateProfileDTO
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string? Username { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters.")]
        public string? FullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [StringLength(500, MinimumLength = 5, ErrorMessage = "Bio must be between 10 and 500 characters")]
        public string?Bio { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 digits")]
        public string? PhoneNumber { get; set; }


        [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
        public string? Address { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateOnly? DateOfBirth { get; set; }

        [NotMapped]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Picture must be a valid image file (jpg, jpeg, png)")]
        public IFormFile? Picture { get; set; } = null;

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Relationship status must be between 3 and 50 characters")]
        public string? Relationship { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Education details must be between 3 and 100 characters")]
        public string? Education { get; set; }

        [RegularExpression("Male|Female|Other", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'")]
        public string? Gender { get; set; }
    }
}
