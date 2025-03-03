using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class AddProfileDTO
{

    [Required(ErrorMessage = "Bio is required")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "Bio must be between 10 and 500 characters")]
    public string Bio { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 digits")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public DateOnly DateOfBirth { get; set; }

    [NotMapped]
    [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Picture must be a valid image file (jpg, jpeg, png)")]
    public IFormFile? Picture { get; set; }

    [Required(ErrorMessage = "Relationship status is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Relationship status must be between 3 and 50 characters")]
    public string Relationship { get; set; }


    [Required(ErrorMessage = "Education details are required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Education details must be between 3 and 100 characters")]
    public string Education { get; set; }


    [RegularExpression("Male|Female|Other", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'")]
    public string? Gender { get; set; }

}