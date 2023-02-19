using System.ComponentModel.DataAnnotations;

namespace CSharpControllerNoDB.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Index number is required")]
        [RegularExpression(@"^s\d+$", ErrorMessage = "Invalid index number format")]
        public string IndexNumber { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]

        // Formatting Date
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Studies is required")]
        public string Studies { get; set; }
        [Required(ErrorMessage = "Study's mode is required")]
        public string Mode { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Father's name is required")]
        public string FathersName { get; set; }

        [Required(ErrorMessage = "Mother's name is required")]
        public string MothersName { get; set; }
    }
}
